using System.Collections.Generic;
using System.IO;
using System.Linq;
using TNRD.StateManagement.Templates;
using UnityEditor;

namespace TNRD.StateManagement
{
    public class StateMachineGraphGenerator
    {
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
        private List<StateMachineGraph.TransitionData> Transitions => stateMachineGraph.Transitions;

        public StateMachineGraphGenerator(StateMachineGraph stateMachineGraph)
        {
            this.stateMachineGraph = stateMachineGraph;
        }

        public static void Generate(StateMachineGraph stateMachineGraph)
        {
            StateMachineGraphGenerator generator = new StateMachineGraphGenerator(stateMachineGraph);

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

            generator.GenerateIds();
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
            Directory.CreateDirectory(Path.Combine(Destination, "Utilities"));
            StateMachineControllerTemplate template = new StateMachineControllerTemplate();
            template.Session = new Dictionary<string, object>();
            AddIncludes(template.Session);
            template.Initialize();
            string transformedText = template.TransformText().TrimStart('\r', '\n');
            File.WriteAllText(Path.Combine(Destination, "Utilities", FullStateMachineName + "Controller.cs"),
                transformedText);
        }

        private void GenerateIds()
        {
            void GenerateStateIds()
            {
                StateIdTemplate template = new StateIdTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(Path.Combine(Destination, "Ids", StateIdName + ".cs"), transformedText);
            }

            void GenerateTransitionIds()
            {
                TransitionIdTemplate template = new TransitionIdTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(Path.Combine(Destination, "Ids", TransitionIdName + ".cs"), transformedText);
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Ids"));

            GenerateStateIds();
            GenerateTransitionIds();
        }

        private void GenerateBase()
        {
            void GenerateBaseState()
            {
                string path = Path.Combine(Destination, "Base", BaseStateName + ".cs");
                if (File.Exists(path))
                    return;

                BaseStateTemplate template = new BaseStateTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(path, transformedText);
            }

            void GenerateBaseTransition()
            {
                string path = Path.Combine(Destination, "Base", BaseTransitionName + ".cs");
                if (File.Exists(path))
                    return;

                BaseTransitionTemplate template = new BaseTransitionTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(path, transformedText);
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Base"));

            GenerateBaseState();
            GenerateBaseTransition();
        }

        private void GenerateStates()
        {
            void GenerateState(StateMachineGraph.StateData stateData)
            {
                string path = Path.Combine(Destination, "States", stateData.Name + "State.cs");
                if (File.Exists(path))
                    return;

                RegularStateTemplate template = new RegularStateTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Session["Name"] = stateData.Name;
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(path, transformedText);
            }

            void GenerateGeneratedState(StateMachineGraph.StateData stateData)
            {
                GeneratedStateTemplate template = new GeneratedStateTemplate();
                template.Session = new Dictionary<string, object>();
                AddIncludes(template.Session);
                template.Session["Name"] = stateData.Name;
                template.Session["Transitions"] = Transitions.Where(x => x.Source == stateData.Name).ToList();
                template.Initialize();
                string transformedText = template.TransformText().TrimStart('\r', '\n');
                File.WriteAllText(Path.Combine(Destination, "States", stateData.Name + "State.g.cs"), transformedText);
            }

            Directory.CreateDirectory(Path.Combine(Destination, "States"));
            foreach (StateMachineGraph.StateData stateData in States)
            {
                GenerateState(stateData);
                GenerateGeneratedState(stateData);
            }
        }

        private void GenerateTransitions()
        {
            void GenerateTransition(StateMachineGraph.TransitionData data)
            {
                foreach (string destination in data.Destinations)
                {
                    string path = Path.Combine(Destination,
                        "Transitions",
                        $"{data.Source}To{destination}" + "Transition.cs");

                    if (File.Exists(path))
                        return;

                    RegularTransitionTemplate template = new RegularTransitionTemplate();
                    template.Session = new Dictionary<string, object>();
                    AddIncludes(template.Session);
                    template.Session["Source"] = data.Source;
                    template.Session["Destination"] = destination;
                    template.Initialize();
                    string transformedText = template.TransformText().TrimStart('\r', '\n');
                    File.WriteAllText(path, transformedText);
                }
            }

            void GenerateGeneratedTransition(StateMachineGraph.TransitionData data)
            {
                foreach (string destination in data.Destinations)
                {
                    GeneratedTransitionTemplate template = new GeneratedTransitionTemplate();
                    template.Session = new Dictionary<string, object>();
                    AddIncludes(template.Session);
                    template.Session["Source"] = data.Source;
                    template.Session["Destination"] = destination;
                    template.Initialize();
                    string transformedText = template.TransformText().TrimStart('\r', '\n');
                    File.WriteAllText(
                        Path.Combine(Destination,
                            "Transitions",
                            $"{data.Source}To{destination}" + "Transition.g.cs"),
                        transformedText);
                }
            }

            Directory.CreateDirectory(Path.Combine(Destination, "Transitions"));

            foreach (StateMachineGraph.TransitionData transitionData in Transitions)
            {
                GenerateTransition(transitionData);
                GenerateGeneratedTransition(transitionData);
            }
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
    }
}
