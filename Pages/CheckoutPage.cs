using Microsoft.Playwright;

public class CheckoutPage {

    private readonly IPage _page;

    public CheckoutPage(IPage page) {
        _page = page;
    }

    public async Task enterFirstName(String firstName) {
        await _page.Locator("//input[@id='first-name']").FillAsync(firstName);
    }

    public async Task enterLastName(String lastName) {
        await _page.Locator("//input[@id='last-name']").FillAsync(lastName);
    }

    public async Task enterZipCode(String zipCode) {
        await _page.Locator("//input[@id='postal-code']").FillAsync(zipCode);
    }

    public async Task clickContinueButton() {
        await _page.Locator("//input[@id='continue']").ClickAsync();
    }

    // validate overview

    public async Task<Boolean> isItemWithQuantityNameAndPriceVisible(String productQuantity, String productName, String productPrice) {
        String locator = "//div[contains(@class, 'cart_list')]/div[contains(@class, 'cart_item')]//div[contains(@class, 'cart_quantity') and normalize-space()='" + productQuantity + "']//ancestor::div[contains(@class, 'cart_item')]//div[contains(@class, 'cart_item_label')]//a[normalize-space()=\"" + productName + "\"]//ancestor::div[contains(@class, 'cart_item_label')]//div[contains(@class, 'inventory_item_price') and normalize-space()='" + productPrice.Replace(" ", "") + "']//ancestor::div[@data-test='inventory-item']";
        return await _page.Locator(locator).IsVisibleAsync();
    }

    // finish button

    public async Task clickFinishButton() {
        await _page.Locator("//button[normalize-space()='Finish']").ClickAsync();
    }

    // checkout complete header

    public async Task<String> getCheckoutCompleteHeader() {
        return await _page.Locator("//h2[contains(@class, 'complete-header')]").TextContentAsync() ?? String.Empty;
    }

    // checkout complete message

    public async Task<String> getCheckoutCompleteMessage() {
        return await _page.Locator("//div[contains(@class, 'complete-text')]").TextContentAsync() ?? String.Empty;
    }

}