namespace Serendip.IK.Configuration.SettingsUI
{
    public class SettingsUIManager
    {
        public static SettingsUIContext Context { get; set; } = new SettingsUIContext();

        public static void RegisterProvider(SettingsUIProvider provider)
        {
            Context.AllItems.AddRange(provider.Register(Context));
        }
    }
}
