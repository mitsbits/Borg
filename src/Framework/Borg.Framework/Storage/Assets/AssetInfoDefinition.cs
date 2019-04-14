using Borg.Framework.Storage.Assets.Contracts;
using System;
using System.Collections.Generic;

namespace Borg.Framework.Storage.Assets
{
    public class AssetInfoDefinition<TKey> : IAssetInfo<TKey> where TKey : IEquatable<TKey>
    {
        private IVersionInfo<TKey> _currentFile;

        public AssetInfoDefinition(TKey id, string name, DocumentBehaviourState state = DocumentBehaviourState.Commited)
        {
            Id = id;
            Name = name;
            DocumentBehaviourState = state;
        }

        public string Name { get; set; }
        public DocumentBehaviourState DocumentBehaviourState { get; }

        public TKey Id { get; }

        IVersionInfo IAssetInfo.CurrentFile => CurrentFile;

        public IVersionInfo<TKey> CurrentFile
        {
            get => _currentFile;
            set => _currentFile = value;
        }

        public IEnumerable<(string key, object value)> Keys => new (string key, object value)[] { (key: nameof(Id), value: Id) };
    }
}