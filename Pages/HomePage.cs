using Microsoft.Playwright;

public class HomePage {

    private readonly IPage _page;

    public HomePage(IPage page) {
        _page = page;
    }

    public async Task<IReadOnlyList<ILocator>> getProductLocators() {
        return await _page.Locator("//div[contains(@class, 'inventory_list')]/div[contains(@class, 'inventory_item')]").AllAsync();
    }

    public async Task clickAddToCartButton(ILocator productLocator) {
        await productLocator.Locator("//button[normalize-space()='Add to cart']").ClickAsync();
    }

    // cart icon

    public async Task<String> getCartItemsCount() {
        return await _page.Locator("//a[contains(@class, 'shopping_cart_link')]//span").TextContentAsync() ?? String.Empty;
    }

    public async Task clickCartPageIcon() {
        await _page.Locator("//a[contains(@class, 'shopping_cart_link')]").ClickAsync();
    }

}