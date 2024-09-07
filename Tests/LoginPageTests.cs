using Microsoft.Playwright;

public class LoginPageTests : TestBase {

    private LoginPage _loginPage;
    private HomePage _homePage;
    private CartPage _cartPage;
    private CheckoutPage _checkoutPage;

    [SetUp]
    public void Init()
    {
        _loginPage = new LoginPage(_page);
        _homePage = new HomePage(_page);
        _cartPage = new CartPage(_page);
        _checkoutPage = new CheckoutPage(_page);
    }

    [Test]
    public async Task testMethod()
    {
        String username = "standard_user";
        String password = "secret_sauce";

        // login
        await _loginPage.enterUsername(username);
        await _loginPage.enterPassword(password);
        await _loginPage.clickLoginButton();

        // get product locators
        IReadOnlyList<ILocator> productLocators = await _homePage.getProductLocators();

        // add product to cart
        await _homePage.clickAddToCartButton(productLocators[0]);

        // add product to cart
        await _homePage.clickAddToCartButton(productLocators[1]);

        // validate cart items count
        String expectedCount = "2";
        Assert.That(await _homePage.getCartItemsCount(), Is.EqualTo(expectedCount));

        // get cart page
        await _homePage.clickCartPageIcon();

        // get cart item locators
        IReadOnlyList<ILocator> cartItemLocators = await _cartPage.getCartItemLocators();

        // remove cart from item
        await _cartPage.clickRemoveButton(cartItemLocators[1]);

        // validate cart items count
        expectedCount = "1";
        Assert.That(await _homePage.getCartItemsCount(), Is.EqualTo(expectedCount));

        // save cart details
        List<Dictionary<String, String>> cartItemDataList = await _cartPage.getCartItemData();

        // click checkout button
        await _cartPage.clickCheckoutButton();

        String firstName = "John";
        String lastName = "Doe";
        String zipCode = "13927";

        // enter checkout details
        await _checkoutPage.enterFirstName(firstName);
        await _checkoutPage.enterLastName(lastName);
        await _checkoutPage.enterZipCode(zipCode);

        // click continue button
        await _checkoutPage.clickContinueButton();

        // validate cart item data
        foreach(Dictionary<String, String> cartItemData in cartItemDataList) {
            String productQuantity = cartItemData["productQuantity"];
            String productName = cartItemData["productName"];
            String productPrice = cartItemData["productPrice"];

            Assert.That(await _checkoutPage.isItemWithQuantityNameAndPriceVisible(productQuantity, productName, productPrice), Is.True);
        }

        // click finish button
        await _checkoutPage.clickFinishButton();

        // validate checkout complete header
        String expectedHeaderValue = "Thank you for your order!";
        Assert.That(expectedHeaderValue, Is.EqualTo(await _checkoutPage.getCheckoutCompleteHeader()));

        // validate checkout complete message
        String expectedMessageValue = "Your order has been dispatched, and will arrive just as fast as the pony can get there!";
        Assert.That(expectedMessageValue, Is.EqualTo(await _checkoutPage.getCheckoutCompleteMessage()));
    }

}