using System;
using System.IO;

namespace MoMoney.Presentation.Model.Projects
{
    public interface IFile
    {
        string path { get; }
        bool does_the_file_exist();
    }

    internal class ApplicationFile : IFile
    {
        public ApplicationFile(string path)
        {
            this.path = path;
        }

        public virtual string path { get; private set; }

        public virtual bool does_the_file_exist()
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }

        public static implicit operator ApplicationFile(string file_path)
        {
            return new ApplicationFile(file_path);
        }

        public static implicit operator string(ApplicationFile file)
        {
            return file.path;
        }

        public bool Equals(ApplicationFile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.path, path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ApplicationFile)) return false;
            return Equals((ApplicationFile) obj);
        }

        public override int GetHashCode()
        {
            return (path != null ? path.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return path;
        }
    }
}