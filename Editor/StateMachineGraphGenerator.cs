using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BrunoMikoski.ScriptableObjectCollections;
using TNRD.StateManagement.Templates;
using TNRD.StateManagement.Templates.State;
using TNRD.StateManagement.Templates.StateMachine;
using TNRD.StateManagement.Templates.Transition;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TNRD.StateManagement
{
    public class StateMachineGraphGenerator
    {
        private const string PREFS_KEY = "TNRD.StateMachineGenerator.CurrentGraph";

        private readonly StateMachineGraph stateMachineGraph;

        private string StateMachineName => stateMachineGraph.StateMachineName;
        private string Namespace => stateMachineGraph.Namespace;
        private string Destination => stateMachineGraph.Destination;
        private string FullStateMachineName => stateMachineGraph.FullStateMachineName;
        private string StateMachineInterfaceName => $"I{FullStateMachineName}";
        private string UpdateProviderName => stateMachineGraph.UpdateProviderName;
        private string UpdateProviderInterfaceName => $"I{UpdateProviderName}";
        private string SubContainerManagerName => stateMachineGraph.SubContainerManagerName;
        private string StateIdName => stateMachineGraph.StateIdName;
        private string TransitionIdName => stateMachineGraph.TransitionIdName;
        private string BaseStateName => stateMachineGraph.BaseStateName;
        private string BaseTransitionName => stateMachineGraph.BaseTransitionName;
        private string StateFactoryName => stateMachineGraph.StateFactoryName;
        private string StateFactoryInterfaceName => $"I{StateFactoryName}";
        private string TransitionFactoryName => stateMachineGraph.TransitionFactoryName;
        private string TransitionFactoryInterfaceName => $"I{TransitionFactoryName}";
        private List<StateMachineGraph.StateData> States => stateMachineGraph.States;
        private List<StateMachineGraph.TransitionData> Transitions => stateMachineGraph.GetTransitions();

        public StateMachineGraphGenerator(StateMachineGraph stateMachineGraph)
        {
            this.stateMachineGraph = stateMachineGraph;
        }

        public static void Generate(StateMachineGraph stateMachineGraph)
        {
            string assetPath = AssetDatabase.GetAssetPath(stateMachineGraph);
            EditorPrefs.SetString(PREFS_KEY, assetPath);

            GeneratePartOne();

            EditorApplication.delayCall += () =>
            {
                if (EditorApplication.isCompiling)
                    return;

                GeneratePartTwoHandler();
            };
        }

        [DidReloadScripts]
        private static void GeneratePartTwoCallback()
        {
            GeneratePartTwoHandler();
        }

        private static StateMachineGraphGenerator CreateGenerator()
        {
            StateMachineGraph stateMachineGraph =
                AssetDatabase.LoadAssetAtPath<StateMachineGraph>(EditorPrefs.GetString(PREFS_KEY));
            return new StateMachineGraphGenerator(stateMachineGraph);
        }

        private static void GeneratePartOne()
        {
            StateMachineGraphGenerator generator = CreateGenerator();
            generator.GenerateIds();
            generator.GenerateConfigurations();
            AssetDatabase.Refresh();
        }

        private static void GeneratePartTwoHandler()
        {
            if (!EditorPrefs.HasKey(PREFS_KEY))
                return;

            try
            {
                GeneratePartTwo();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                EditorPrefs.DeleteKey(PREFS_KEY);
            }
        }

        private static void GeneratePartTwo()
        {
            StateMachineGraphGenerator generator = CreateGenerator();

            generator.GenerateStateIdItems();
            generator.GenerateTransitionIdItems();
            generator.GenerateConfigurationItems();

            generator.GenerateStateMachine();
            generator.GenerateInterfaces();
            generator.GenerateFactories();

#if HAS_ZENJECT
            if (stateMachineGraph.UseZenject)
            {
                generator.GenerateZenjectContainerManager();
                generator.GenerateZenjectInstallers();
                generator.GenerateZenjectUpdateProvider();
                generator.GenerateZenjectInitializer();
            }
            else
            {
                generator.GenerateStateMachineController();
            }
#else
            generator.GenerateStateMachineController();
#endif

            generator.GenerateBase();
            generator.GenerateStates();
            generator.GenerateTransitions();

            AssetDatabase.Refresh();
        }

        private void GenerateStateMachine()
        {
            void GenerateRegularStateMachine()
            {
                string path = Path.Combine(Destination, FullStateMachineName + ".cs");
                if (File.Exists(path))
                    return;

                RegularStateMachineTemplate template = new RegularStateMachineTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');

                File.WriteAllText(path, transformedText);
            }

            void GenerateGeneratedStateMachine()
            {
                GeneratedStateMachineTemplate template = new GeneratedStateMachineTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(Path.Combine(Destination, FullStateMachineName + ".g.cs"), transformedText);
            }

            GenerateRegularStateMachine();
            GenerateGeneratedStateMachine();
        }

        private void GenerateInterfaces()
        {
            void GenerateStateMachineInterface()
            {
                string path = Path.Combine(Destination, "Contracts", StateMachineInterfaceName + ".cs");
                if (File.Exists(path))
                    return;

                StateMachineInterfaceTemplate template = new StateMachineInterfaceTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');

                File.WriteAllText(path, transformedText);
            }

            void GenerateStateFactoryInterface()
            {
                string path = Path.Combine(Destination, "Contracts", StateFactoryInterfaceName + ".cs");
                if (File.Exists(path))
                    return;

                StateFactoryInterfaceTemplate template = new StateFactoryInterfaceTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');

                File.WriteAllText(path, transformedText);
            }

            void GenerateTransitionFactoryInterface()
            {
                string path = Path.Combine(Destination, "Contracts", TransitionFactoryInterfaceName + ".cs");
                if (File.Exists(path))
                    return;

                TransitionFactoryInterfaceTemplate template = new TransitionFactoryInterfaceTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');

                File.WriteAllText(path, transformedText);
            }

            void GenerateUpdateProviderInterface()
            {
                string path = Path.Combine(Destination, "Contracts", UpdateProviderInterfaceName + ".cs");
                if (File.Exists(path))
                    return;

                UpdateProviderInterfaceTemplate template = new UpdateProviderInterfaceTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');

                File.WriteAllText(path, transformedText);
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Contracts"));

            GenerateStateMachineInterface();
            GenerateStateFactoryInterface();
            GenerateTransitionFactoryInterface();
            GenerateUpdateProviderInterface();
        }

        private void GenerateFactories()
        {
#if HAS_ZENJECT
            void GenerateZenjectStateFactory()
            {
                string path = Path.Combine(Destination, "Factories", StateFactoryName + ".cs");
                if (File.Exists(path))
                    return;

                ZenjectStateFactoryTemplate template = new ZenjectStateFactoryTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(path, transformedText);
            }

            void GenerateZenjectTransitionFactory()
            {
                string path = Path.Combine(Destination, "Factories", TransitionFactoryName + ".cs");
                if (File.Exists(path))
                    return;

                ZenjectTransitionFactoryTemplate template = new ZenjectTransitionFactoryTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(path, transformedText);
            }
#endif

            void GenerateBasicStateFactory()
            {
                string path = Path.Combine(Destination, "Factories", StateFactoryName + ".cs");
                if (File.Exists(path))
                    return;

                BasicStateFactoryTemplate template = new BasicStateFactoryTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(path, transformedText);
            }

            void GenerateBasicTransitionFactory()
            {
                string path = Path.Combine(Destination, "Factories", TransitionFactoryName + ".cs");
                if (File.Exists(path))
                    return;

                BasicTransitionFactoryTemplate template = new BasicTransitionFactoryTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(path, transformedText);
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Factories"));

#if HAS_ZENJECT
            if (stateMachineGraph.UseZenject)
            {
                GenerateZenjectStateFactory();
                GenerateZenjectTransitionFactory();
            }
            else
            {
                GenerateBasicStateFactory();
                GenerateBasicTransitionFactory();
            }
#else
            GenerateBasicStateFactory();
            GenerateBasicTransitionFactory();
#endif
        }

#if HAS_ZENJECT
        private void GenerateZenjectUpdateProvider()
        {
            Directory.CreateDirectory(Path.Combine(Destination, "Utilities"));
            ZenjectUpdateProviderTemplate template = new ZenjectUpdateProviderTemplate();
            template.Session = new Dictionary<string, object>();
            AddIncludes(template.Session);
            template.Initialize();
            string transformedText = template.TransformText().TrimStart('\r', '\n');
            File.WriteAllText(Path.Combine(Destination, "Utilities", UpdateProviderName + ".cs"), transformedText);
        }

        private void GenerateZenjectContainerManager()
        {
            Directory.CreateDirectory(Path.Combine(Destination, "Utilities"));
            ContainerManagerTemplate template = new ContainerManagerTemplate();
            template.Session = new Dictionary<string, object>();
            AddIncludes(template.Session);
            template.Initialize();
            string transformedText = template.TransformText().TrimStart('\r', '\n');
            File.WriteAllText(Path.Combine(Destination, "Utilities", SubContainerManagerName + ".cs"), transformedText);
        }

        private void GenerateZenjectInstallers()
        {
            void GenerateStateComponentInstaller()
            {
                ZenjectStateComponentInstallerTemplate template = new ZenjectStateComponentInstallerTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(
                    Path.Combine(Destination, "Installers", StateMachineName + "StateComponentInstaller.cs"),
                    transformedText);
            }

            void GenerateTransitionComponentInstaller()
            {
                ZenjectTransitionComponentInstallerTemplate
                    template = new ZenjectTransitionComponentInstallerTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(
                    Path.Combine(Destination, "Installers", StateMachineName + "TransitionComponentInstaller.cs"),
                    transformedText);
            }

            void GenerateStateMachineInstaller()
            {
                ZenjectStateMachineInstallerTemplate template = new ZenjectStateMachineInstallerTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(Path.Combine(Destination, "Installers", FullStateMachineName + "Installer.cs"),
                    transformedText);
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Installers"));

            GenerateStateMachineInstaller();
            GenerateStateComponentInstaller();
            GenerateTransitionComponentInstaller();
        }

        private void GenerateZenjectInitializer()
        {
            Directory.CreateDirectory(Path.Combine(Destination, "Utilities"));
            ZenjectStateMachineInitializerTemplate template = new ZenjectStateMachineInitializerTemplate();
            template.Session = new Dictionary<string, object>();
            AddIncludes(template.Session);
            template.Initialize();
            string transformedText = template.TransformText().TrimStart('\r', '\n');
            File.WriteAllText(Path.Combine(Destination, "Utilities", FullStateMachineName + "Initializer.cs"),
                transformedText);
        }
#endif

        private void GenerateStateMachineController()
        {
            void GenerateRegular()
            {
                GenerateAndSave<StateMachineControllerTemplate>(
                    Path.Combine(Destination, "Utilities", FullStateMachineName + "Controller.cs"));
            }

            void GenerateGenerated()
            {
                GenerateAndSave<GeneratedStateMachineControllerTemplate>(
                    Path.Combine(Destination, "Utilities", FullStateMachineName + "Controller.g.cs"));
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Utilities"));

            GenerateRegular();
            GenerateGenerated();
        }

        private void GenerateIds()
        {
            void GenerateStateIds()
            {
                void GenerateStateIdItem()
                {
                    GenerateAndSave<StateIdCollectionItemTemplate>(
                        Path.Combine(Destination, "Ids", StateIdName + ".cs"));
                }

                void GenerateStateIdCollection()
                {
                    GenerateAndSave<StateIdCollectionTemplate>(
                        Path.Combine(Destination, "Ids", StateIdName + "Collection.cs"));
                }

                GenerateStateIdCollection();
                GenerateStateIdItem();
            }

            void GenerateTransitionIds()
            {
                void GenerateTransitionIdItem()
                {
                    GenerateAndSave<TransitionIdCollectionItemTemplate>(
                        Path.Combine(Destination, "Ids", TransitionIdName + ".cs"));
                }

                void GenerateTransitionIdCollection()
                {
                    GenerateAndSave<TransitionIdCollectionTemplate>(
                        Path.Combine(Destination, "Ids", TransitionIdName + "Collection.cs"));
                }

                GenerateTransitionIdCollection();
                GenerateTransitionIdItem();
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Ids"));

            GenerateStateIds();
            GenerateTransitionIds();
        }

        private void GenerateConfigurations()
        {
            void GenerateStateConfigurations()
            {
                void GenerateRegularBaseStateConfiguration()
                {
                    GenerateAndSave<BaseStateConfigurationTemplate>(
                        Path.Combine(Destination,
                            "Configurations",
                            "Base" + StateMachineName + "StateConfiguration.cs"),
                        false);
                }

                void GenerateGeneratedBaseStateConfiguration()
                {
                    GenerateAndSave<GeneratedBaseStateConfigurationTemplate>(
                        Path.Combine(Destination,
                            "Configurations",
                            "Base" + StateMachineName + "StateConfiguration.g.cs"));
                }

                void GenerateRegularStateConfigurationItems()
                {
                    foreach (StateMachineGraph.StateData stateData in States)
                    {
                        GenerateAndSave<CustomStateConfigurationTemplate>(
                            Path.Combine(Destination, "Configurations", stateData.Name + "StateConfiguration.cs"),
                            false,
                            new KeyValuePair<string, object>("Custom", stateData.Name));
                    }
                }

                void GenerateGeneratedStateConfigurationItems()
                {
                    foreach (StateMachineGraph.StateData stateData in States)
                    {
                        GenerateAndSave<GeneratedCustomStateConfigurationTemplate>(
                            Path.Combine(Destination, "Configurations", stateData.Name + "StateConfiguration.g.cs"),
                            true,
                            new KeyValuePair<string, object>("Custom", stateData.Name));
                    }
                }

                void GenerateStateConfigurationCollection()
                {
                    GenerateAndSave<StateConfigurationsTemplate>(
                        Path.Combine(Destination, "Configurations", StateMachineName + "StateConfigurations.cs"));
                }

                Directory.CreateDirectory(Path.Combine(Destination, "Configurations"));

                GenerateRegularBaseStateConfiguration();
                GenerateGeneratedBaseStateConfiguration();
                GenerateRegularStateConfigurationItems();
                GenerateGeneratedStateConfigurationItems();
                GenerateStateConfigurationCollection();
            }

            void GenerateTransitionConfigurations()
            {
                void GenerateRegularBaseTransitionConfiguration()
                {
                    GenerateAndSave<BaseTransitionConfigurationTemplate>(
                        Path.Combine(Destination,
                            "Configurations",
                            "Base" + StateMachineName + "TransitionConfiguration.cs"),
                        false);
                }

                void GenerateGeneratedBaseTransitionConfiguration()
                {
                    GenerateAndSave<GeneratedBaseTransitionConfigurationTemplate>(
                        Path.Combine(Destination,
                            "Configurations",
                            "Base" + StateMachineName + "TransitionConfiguration.g.cs"));
                }

                void GenerateRegularTransitionConfigurationItems()
                {
                    foreach (StateMachineGraph.TransitionData transitionData in Transitions)
                    {
                        foreach (string destination in transitionData.Destinations)
                        {
                            string name = transitionData.Source + "To" + destination;
                            GenerateAndSave<CustomTransitionConfigurationTemplate>(
                                Path.Combine(Destination, "Configurations", name + "TransitionConfiguration.cs"),
                                false,
                                new KeyValuePair<string, object>("Custom", name));
                        }
                    }
                }

                void GenerateGeneratedStateConfigurationItems()
                {
                    foreach (StateMachineGraph.TransitionData transitionData in Transitions)
                    {
                        foreach (string destination in transitionData.Destinations)
                        {
                            string name = transitionData.Source + "To" + destination;
                            GenerateAndSave<GeneratedCustomTransitionConfigurationTemplate>(
                                Path.Combine(Destination, "Configurations", name + "TransitionConfiguration.g.cs"),
                                true,
                                new KeyValuePair<string, object>("Custom", name));
                        }
                    }
                }

                void GenerateTransitionConfigurationCollection()
                {
                    GenerateAndSave<TransitionConfigurationsTemplate>(
                        Path.Combine(Destination, "Configurations", StateMachineName + "TransitionConfigurations.cs"));
                }

                Directory.CreateDirectory(Path.Combine(Destination, "Configurations"));

                GenerateRegularBaseTransitionConfiguration();
                GenerateGeneratedBaseTransitionConfiguration();
                GenerateRegularTransitionConfigurationItems();
                GenerateGeneratedStateConfigurationItems();
                GenerateTransitionConfigurationCollection();
            }

            GenerateStateConfigurations();
            GenerateTransitionConfigurations();
        }

        private void GenerateConfigurationItems()
        {
            void GenerateStateConfigurationItems()
            {
                TypeCache.TypeCollection itemTypes = TypeCache.GetTypesDerivedFrom<ScriptableObjectCollectionItem>();
                TypeCache.TypeCollection collectionTypes = TypeCache.GetTypesDerivedFrom<ScriptableObjectCollection>();

                Type itemType = itemTypes.First(x => x.Name == "Base" + StateMachineName + "StateConfiguration");
                Type collectionType = collectionTypes.First(x => x.Name == StateMachineName + "StateConfigurations");
                if (!CollectionsRegistry.Instance.TryGetCollectionFromItemType(itemType,
                        out ScriptableObjectCollection collection))
                {
                    collection =
                        (ScriptableObjectCollection)ScriptableObjectCollectionUtility.CreateScriptableObjectOfType(
                            collectionType,
                            Destination + "/Configurations/",
                            StateMachineName + "StateConfigurations");
                }

                foreach (StateMachineGraph.StateData stateData in States)
                {
                    string name = stateData.Name + "StateConfiguration";

                    if (collection.TryGetItemByName(name, out _))
                        continue;

                    Type configurationType = itemTypes.First(x => x.Name == name);
                    collection.AddNew(configurationType, name);
                }

                FixCollection(collection, "Configurations");
                CodeGenerationUtility.GenerateStaticCollectionScript(collection);
            }

            void GenerateTransitionConfigurationItems()
            {
                TypeCache.TypeCollection itemTypes = TypeCache.GetTypesDerivedFrom<ScriptableObjectCollectionItem>();
                TypeCache.TypeCollection collectionTypes = TypeCache.GetTypesDerivedFrom<ScriptableObjectCollection>();

                Type itemType = itemTypes.First(x => x.Name == "Base" + StateMachineName + "TransitionConfiguration");
                Type collectionType =
                    collectionTypes.First(x => x.Name == StateMachineName + "TransitionConfigurations");
                if (!CollectionsRegistry.Instance.TryGetCollectionFromItemType(itemType,
                        out ScriptableObjectCollection collection))
                {
                    collection =
                        (ScriptableObjectCollection)ScriptableObjectCollectionUtility.CreateScriptableObjectOfType(
                            collectionType,
                            Destination + "/Configurations/",
                            StateMachineName + "TransitionConfigurations");
                }

                foreach (StateMachineGraph.TransitionData transitionData in Transitions)
                {
                    foreach (string destination in transitionData.Destinations)
                    {
                        string name = transitionData.Source + "To" + destination + "TransitionConfiguration";

                        if (collection.TryGetItemByName(name, out _))
                            continue;

                        Type configurationType = itemTypes.First(x => x.Name == name);
                        collection.AddNew(configurationType, name);
                    }
                }

                FixCollection(collection, "Configurations");
                CodeGenerationUtility.GenerateStaticCollectionScript(collection);
            }

            GenerateStateConfigurationItems();
            GenerateTransitionConfigurationItems();
        }

        private void GenerateBase()
        {
            void GenerateBaseState()
            {
                GenerateAndSave<BaseStateTemplate>(
                    Path.Combine(Destination, "Base", BaseStateName + ".cs"),
                    false);
            }

            void GenerateBaseTransition()
            {
                GenerateAndSave<BaseTransitionTemplate>(
                    Path.Combine(Destination, "Base", BaseTransitionName + ".cs"),
                    false);
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Base"));

            GenerateBaseState();
            GenerateBaseTransition();
        }

        private void GenerateStates()
        {
            void GenerateState(StateMachineGraph.StateData stateData)
            {
                GenerateAndSave<RegularStateTemplate>(
                    Path.Combine(Destination, "States", stateData.Name + "State.cs"),
                    false,
                    new KeyValuePair<string, object>("Name", stateData.Name));
            }

            void GenerateGeneratedState(StateMachineGraph.StateData stateData)
            {
                GenerateAndSave<GeneratedStateTemplate>(
                    Path.Combine(Destination, "States", stateData.Name + "State.g.cs"),
                    true,
                    new KeyValuePair<string, object>("Name", stateData.Name),
                    new KeyValuePair<string, object>("Transitions",
                        Transitions.Where(x => x.Source == stateData.Name).ToList()));
            }

            Directory.CreateDirectory(Path.Combine(Destination, "States"));
            foreach (StateMachineGraph.StateData stateData in States)
            {
                GenerateState(stateData);
                GenerateGeneratedState(stateData);
            }
        }

        private void GenerateStateIdItems()
        {
            TypeCache.TypeCollection itemTypes = TypeCache.GetTypesDerivedFrom<ScriptableObjectCollectionItem>();
            TypeCache.TypeCollection collectionTypes = TypeCache.GetTypesDerivedFrom<ScriptableObjectCollection>();

            Type itemType = itemTypes.First(x => x.Name == StateIdName);
            Type collectionType = collectionTypes.First(x => x.Name == StateIdName + "Collection");
            if (!CollectionsRegistry.Instance.TryGetCollectionFromItemType(itemType,
                    out ScriptableObjectCollection collection))
            {
                collection =
                    (ScriptableObjectCollection)ScriptableObjectCollectionUtility.CreateScriptableObjectOfType(
                        collectionType,
                        Destination + "/Ids/",
                        StateIdName + "Collection");
            }

            foreach (StateMachineGraph.StateData stateData in States)
            {
                if (!collection.TryGetItemByName(stateData.Name, out _))
                    collection.AddNew(itemType, stateData.Name);
            }

            FixCollection(collection, "Ids");
            CodeGenerationUtility.GenerateStaticCollectionScript(collection);
        }

        private void GenerateTransitions()
        {
            void GenerateTransition(StateMachineGraph.TransitionData data)
            {
                foreach (string destination in data.Destinations)
                {
                    GenerateAndSave<RegularTransitionTemplate>(
                        Path.Combine(Destination, "Transitions", $"{data.Source}To{destination}" + "Transition.cs"),
                        false,
                        new KeyValuePair<string, object>("Source", data.Source),
                        new KeyValuePair<string, object>("Destination", destination));
                }
            }

            void GenerateGeneratedTransition(StateMachineGraph.TransitionData data)
            {
                foreach (string destination in data.Destinations)
                {
                    GenerateAndSave<GeneratedTransitionTemplate>(
                        Path.Combine(Destination, "Transitions", $"{data.Source}To{destination}" + "Transition.g.cs"),
                        true,
                        new KeyValuePair<string, object>("Source", data.Source),
                        new KeyValuePair<string, object>("Destination", destination));
                }
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Transitions"));

            foreach (StateMachineGraph.TransitionData transitionData in Transitions)
            {
                GenerateTransition(transitionData);
                GenerateGeneratedTransition(transitionData);
            }
        }

        private void GenerateTransitionIdItems()
        {
            TypeCache.TypeCollection itemTypes = TypeCache.GetTypesDerivedFrom<ScriptableObjectCollectionItem>();
            TypeCache.TypeCollection collectionTypes = TypeCache.GetTypesDerivedFrom<ScriptableObjectCollection>();

            Type transitionIdType = itemTypes.First(x => x.Name == TransitionIdName);
            Type transitionIdCollectionType =
                collectionTypes.First(x => x.Name == TransitionIdName + "Collection");

            if (!CollectionsRegistry.Instance.TryGetCollectionFromItemType(transitionIdType,
                    out ScriptableObjectCollection transitionIdCollection))
            {
                transitionIdCollection =
                    (ScriptableObjectCollection)ScriptableObjectCollectionUtility.CreateScriptableObjectOfType(
                        transitionIdCollectionType,
                        Destination + "/Ids/",
                        TransitionIdName + "Collection");
            }

            foreach (StateMachineGraph.TransitionData transitionData in Transitions)
            {
                foreach (string destination in transitionData.Destinations)
                {
                    string name = $"{transitionData.Source}To{destination}";
                    if (!transitionIdCollection.TryGetItemByName(name, out _))
                        transitionIdCollection.AddNew(transitionIdType, name);
                }
            }

            FixCollection(transitionIdCollection, "Ids");
            CodeGenerationUtility.GenerateStaticCollectionScript(transitionIdCollection);
        }

        private void AddIncludes(IDictionary<string, object> session)
        {
            session[nameof(Namespace)] = Namespace;
            session[nameof(StateMachineName)] = StateMachineName;
            session[nameof(FullStateMachineName)] = FullStateMachineName;
            session[nameof(StateMachineInterfaceName)] = StateMachineInterfaceName;
            session["StateMachineNameLowercase"] = StateMachineName.LowerCaseFirstChar();
            session["FullStateMachineNameLowercase"] = FullStateMachineName.LowerCaseFirstChar();
            session[nameof(SubContainerManagerName)] = SubContainerManagerName;
            session[nameof(StateIdName)] = StateIdName;
            session[nameof(TransitionIdName)] = TransitionIdName;
            session["InitialStateName"] = States.First(x => x.IsInitialState).Name;
            session[nameof(BaseStateName)] = BaseStateName;
            session[nameof(BaseTransitionName)] = BaseTransitionName;
            session[nameof(StateFactoryName)] = StateFactoryName;
            session[nameof(StateFactoryInterfaceName)] = StateFactoryInterfaceName;
            session[nameof(TransitionFactoryName)] = TransitionFactoryName;
            session[nameof(TransitionFactoryInterfaceName)] = TransitionFactoryInterfaceName;
            session[nameof(UpdateProviderName)] = UpdateProviderName;
            session[nameof(UpdateProviderInterfaceName)] = UpdateProviderInterfaceName;
            session[nameof(States)] = States;
            session[nameof(Transitions)] = Transitions;
        }

        private void FixCollection(ScriptableObjectCollection collection, string folder)
        {
            void CheckGeneratedCodeLocation(SerializedObject serializedObject)
            {
                if (!string.IsNullOrEmpty(serializedObject.FindProperty("generatedFileLocationPath").stringValue))
                    return;

                if (!string.IsNullOrEmpty(SOCSettings.Instance.GeneratedScriptsDefaultFilePath))
                {
                    serializedObject.FindProperty("generatedFileLocationPath").stringValue =
                        SOCSettings.Instance.GeneratedScriptsDefaultFilePath;
                }
                else
                {
                    string collectionScriptPath =
                        Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(collection)));

                    serializedObject.FindProperty("generatedFileLocationPath").stringValue = collectionScriptPath;
                }
            }

            void CheckGeneratedStaticFileName(SerializedObject serializedObject)
            {
                if (!string.IsNullOrEmpty(serializedObject.FindProperty("generatedStaticClassFileName").stringValue))
                    return;

                if (collection.name.Equals(collection.GetItemType().Name, StringComparison.Ordinal)
                    && serializedObject.FindProperty("generateAsPartialClass").boolValue)
                {
                    serializedObject.FindProperty("generatedStaticClassFileName").stringValue =
                        $"{collection.GetItemType().Name}Static";
                }
                else
                {
                    serializedObject.FindProperty("generatedStaticClassFileName").stringValue =
                        $"{collection.name}Static".Sanitize().FirstToUpper();
                }

                serializedObject.ApplyModifiedProperties();
            }

            void ValidateGeneratedFileNamespace(SerializedObject serializedObject)
            {
                if (string.IsNullOrEmpty(serializedObject.FindProperty("generateStaticFileNamespace").stringValue))
                {
                    if (collection != null)
                    {
                        string targetNamespace = collection.GetItemType().Namespace;
                        if (!string.IsNullOrEmpty(targetNamespace))
                        {
                            serializedObject.FindProperty("generateStaticFileNamespace").stringValue = targetNamespace;
                            serializedObject.ApplyModifiedProperties();
                        }
                    }
                }
            }

            SerializedObject serializedObject = new SerializedObject(collection);

            SerializedProperty serializedProperty = serializedObject.FindProperty("generatedFileLocationPath");
            serializedProperty.stringValue = Destination + "/" + folder;

            CheckGeneratedCodeLocation(serializedObject);
            CheckGeneratedStaticFileName(serializedObject);
            ValidateGeneratedFileNamespace(serializedObject);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void GenerateAndSave<TGenerator>(
            string path,
            bool overwrite = true,
            params KeyValuePair<string, object>[] sessionValues
        )
            where TGenerator : new()
        {
            Type generatorType = typeof(TGenerator);

            TGenerator generator = new TGenerator();

            Dictionary<string, object> templateSession = new();
            AddIncludes(templateSession);

            foreach (KeyValuePair<string, object> sessionValue in sessionValues)
                templateSession[sessionValue.Key] = sessionValue.Value;

            generatorType
                .GetProperty("Session", BindingFlags.Instance | BindingFlags.Public)
                !.SetValue(generator, templateSession);

            generatorType
                .GetMethod("Initialize", BindingFlags.Instance | BindingFlags.Public)
                !.Invoke(generator, null);

            string output = (string)generatorType
                .GetMethod("TransformText", BindingFlags.Instance | BindingFlags.Public)
                !.Invoke(generator, null);

            output = output.TrimStart('\r', '\n');

            if (File.Exists(path) && !overwrite)
                return;

            File.WriteAllText(path, output);
        }
    }
}
