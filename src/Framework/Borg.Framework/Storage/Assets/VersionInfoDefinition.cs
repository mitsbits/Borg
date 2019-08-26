using Borg.Framework.Storage.Assets.Contracts;
using Borg.Framework.Storage.Contracts;
using System;

namespace Borg.Framework.Storage.Assets
{
    public abstract class VersionInfoDefinition 
    {
        public VersionInfoDefinition(int version)
        {
            Version = version;
        }

        public int Version { get; }

    }

    public class VersionInfoDefinition<TKey> : VersionInfoDefinition, IVersionInfo<TKey> where TKey : IEquatable<TKey>
    {
        public VersionInfoDefinition(int version, FileSpecDefinition<TKey> fileSpec) : base(version)
        {
            FileSpec = fileSpec;
        }

        public virtual FileSpecDefinition<TKey> FileSpec { get; protected set; }


        IFileSpec<TKey> IVersionInfo<TKey>.FileSpec => FileSpec;

        IFileSpec IVersionInfo.FileSpec => FileSpec;
    }
}