
namespace BrokenFeatures.Config {
    public class AddedContent : IUpdatableSettings {
        public SettingGroup WizardAbilities = new SettingGroup();

        public void OverrideSettings(IUpdatableSettings userSettings) {
            var loadedSettings = userSettings as AddedContent;
            WizardAbilities.LoadSettingGroup(loadedSettings.WizardAbilities);
        }
    }
}
