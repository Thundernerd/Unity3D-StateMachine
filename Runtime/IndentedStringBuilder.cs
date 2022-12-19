using System;
using System.Text;

namespace TNRD.StateManagement
{
    public class IndentedStringBuilder
    {
        private class IndentScope : IDisposable
        {
            private readonly IndentedStringBuilder indentedStringBuilder;

            public IndentScope(IndentedStringBuilder indentedStringBuilder)
            {
                this.indentedStringBuilder = indentedStringBuilder;
                this.indentedStringBuilder.IncrementIndent();
            }

            /// <inheritdoc />
            public void Dispose()
            {
                indentedStringBuilder.DecrementIndent();
            }
        }

        private readonly string indentCharacter;
        private readonly StringBuilder stringBuilder;
        private int indentLevel;

        public IndentedStringBuilder(string indentCharacter = "    ")
        {
            this.indentCharacter = indentCharacter;

            stringBuilder = new StringBuilder();
            indentLevel = 0;
        }

        public IDisposable Indent()
        {
            return new IndentScope(this);
        }

        public void IncrementIndent()
        {
            indentLevel++;
        }

        public void DecrementIndent()
        {
            if (indentLevel > 0)
                indentLevel--;
        }

        public void Append(string value)
        {
            stringBuilder.Append(value);
        }

        public void AppendLine()
        {
            stringBuilder.AppendLine();
        }

        public void AppendLine(string value)
        {
            stringBuilder.AppendLine(GetIndent() + value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        private string GetIndent()
        {
            string indent = string.Empty;

            for (int i = 0; i < indentLevel; i++)
            {
                indent += indentCharacter;
            }

            return indent;
        }
    }
}
