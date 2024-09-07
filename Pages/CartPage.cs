using Microsoft.Playwright;

public class CartPage {

    private readonly IPage _page;

    public CartPage(IPage page) {
        _page = page;
    }
    
    // get all cart item locators

    public async Task<IReadOnlyList<ILocator>> getCartItemLocators() {
        return await _page.Locator("//div[contains(@class, 'cart_list')]/div[contains(@class, 'cart_item')]").AllAsync();
    }

    // remove button

    public async Task clickRemoveButton(ILocator cartItemLocator) {
        await cartItemLocator.Locator("//button[normalize-space()='Remove']").ClickAsync();
    }

    // product quantity

    public async Task<String> getProductQuantity(ILocator cartItemLocator) {
        return await _page.Locator("//div[contains(@class, 'cart_list')]/div[contains(@class, 'cart_item')]//div[contains(@class, 'cart_quantity')]").TextContentAsync() ?? String.Empty;
    }

    // product name

    public async Task<String> getProductName(ILocator cartItemLocator) {
        return await _page.Locator("//div[contains(@class, 'cart_list')]/div[contains(@class, 'cart_item')]//div[contains(@class, 'cart_item_label')]//a").TextContentAsync() ?? String.Empty;
    }

    // product price

    public async Task<String> getProductPrice(ILocator cartItemLocator) {
        return await _page.Locator("//div[contains(@class, 'cart_list')]/div[contains(@class, 'cart_item')]//div[contains(@class, 'inventory_item_price')]").TextContentAsync() ?? String.Empty;
    }

    // get product data

    public async Task<List<Dictionary<string, string>>> getCartItemData() {
        List<Dictionary<String, String>> cartItemDataList = new List<Dictionary<string, string>>();

        foreach(ILocator cartItemLocator in await getCartItemLocators()) {
            Dictionary<String, String> cartItemData = new Dictionary<string, string>
            {
                { "productQuantity", await getProductQuantity(cartItemLocator) },
                { "productName", await getProductName(cartItemLocator) },
                { "productPrice", await getProductPrice(cartItemLocator) }
            };

            cartItemDataList.Add(cartItemData);
        }
        
        return cartItemDataList;
    }

    public async Task clickCheckoutButton() {
        await _page.Locator("//button[normalize-space()='Checkout']").ClickAsync();
    }

}