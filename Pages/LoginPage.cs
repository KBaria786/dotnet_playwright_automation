using Microsoft.Playwright;

public class LoginPage {

    private readonly IPage _page;

    public LoginPage(IPage page) {
        _page = page;
    }

    public async Task enterUsername(String username) {
        await _page.Locator("//input[@id='user-name']").FillAsync(username);
    }

    public async Task enterPassword(String password) {
        await _page.Locator("//input[@id='password']").FillAsync(password);
    }
    
    public async Task clickLoginButton() {
        await _page.Locator("//input[@id='login-button']").ClickAsync();
    }

}