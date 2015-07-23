﻿using System;
using System.Linq;
using System.Windows.Forms;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace Sdl.Community.AddSourceTM
{
    [TranslationProviderWinFormsUi(
        Id = "Sdl.Community.AddSourceTMWinFormsUI",
        Name = "Sdl.Community.AddSourceTMWinFormsUI",
        Description = "Sdl.Community.AddSourceTMWinFormsUI")]
    public class AddSourceTmTranslationProviderWinFormsUi : ITranslationProviderWinFormsUI
    {
        private ITranslationProviderWinFormsUI FileBasedTranslationProviderUi
        {
            get
            {
                return TranslationProviderManager.GetTranslationProviderWinFormsUIs().First(x => x.TypeName.Equals("File-based translation memory"));
            }
        }

        #region ITranslationProviderWinFormsUI Members

        public ITranslationProvider[] Browse(IWin32Window owner, LanguagePair[] languagePairs, ITranslationProviderCredentialStore credentialStore)
        {
            var translationProviders = FileBasedTranslationProviderUi.Browse(owner, languagePairs, credentialStore);

            return new ITranslationProvider[] {new AddSourceTmTranslationProvider(translationProviders[0])};
        }

        public bool Edit(IWin32Window owner, ITranslationProvider translationProvider, LanguagePair[] languagePairs, ITranslationProviderCredentialStore credentialStore)
        {
            var tProvider = translationProvider as AddSourceTmTranslationProvider;
            return tProvider != null && FileBasedTranslationProviderUi.Edit(owner, tProvider.FileBasedTranslationProvider, languagePairs, credentialStore);
        }

        public bool GetCredentialsFromUser(IWin32Window owner, Uri translationProviderUri, string translationProviderState, ITranslationProviderCredentialStore credentialStore)
        {
            return false;
        }

        public TranslationProviderDisplayInfo GetDisplayInfo(Uri translationProviderUri, string translationProviderState)
        {
            return FileBasedTranslationProviderUi.GetDisplayInfo(new Uri("sdltm.file://"+translationProviderUri.AbsolutePath), 
                translationProviderState);
          
        }

        public bool SupportsEditing
        {
            get { return true; }
        }

        public bool SupportsTranslationProviderUri(Uri translationProviderUri)
        {
            return translationProviderUri.Scheme.Equals(AddSourceTmTranslationProvider.ProviderUriScheme, StringComparison.InvariantCultureIgnoreCase);
        }

        public string TypeDescription
        {
            get { return "Translation provider that will create or update \"Source File\" custom filed of the TM"; }
        }

        public string TypeName
        {
            get { return "AddSourceTM ..."; }
        }

        #endregion
    }
}