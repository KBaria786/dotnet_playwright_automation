using Microsoft.Playwright;

public class TestBase {

    protected IPlaywright _playwright;
    protected IBrowser _browser;
    protected IPage _page;

    [SetUp]
    public async Task setup() {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {Headless = false, SlowMo = 1000});
        _page = await _browser.NewPageAsync();
        await _page.GotoAsync("https://www.saucedemo.com/");
    }

    [TearDown]
    public async Task teardown() {
        // await Task.Delay(30 * 1000);
        await _page.CloseAsync();
        await _browser.CloseAsync();
    }

}