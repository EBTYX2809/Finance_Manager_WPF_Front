using Finance_Manager_WPF_Front.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_WPF_Front.Views;

public static class CurrencyCultureProvider
{
    private static UserSession _userSession;

    // Хардкод словаря популярных валют и регионов
    private static readonly Dictionary<string, string> _defaultCurrencyCultures = new(StringComparer.OrdinalIgnoreCase)
    {
        { "UAH", "uk-UA" }, // Гривны - Украина
        { "USD", "en-US" }, // Доллары - США
        { "RUB", "ru-RU" }, // Рубли - Россия
        { "EUR", "de-DE" }, // Евро - Европа (выбрал Германия как пример)
        { "PLN", "pl-PL" }, // Злотые - Польша
        { "GBP", "en-GB" }, // Фунты стерлингов - Великобритания
        { "JPY", "ja-JP" }, // Иена - Япония
        { "CNY", "zh-CN" }, // Юань - Китай
        { "INR", "hi-IN" }, // Индийская рупия - Индия
        { "CAD", "en-CA" }, // Канадский доллар - Канада
        { "AUD", "en-AU" }, // Австралийский доллар - Австралия
        { "CHF", "de-CH" }, // Швейцарский франк - Швейцария
        { "NZD", "en-NZ" }, // Новозеландский доллар - Новая Зеландия
        { "SEK", "sv-SE" }, // Шведская крона - Швеция
        { "KRW", "ko-KR" }, // Южнокорейская вона - Южная Корея
    };

    public static void Initialize(UserSession userSession)
    {
        if (_userSession != null)
            throw new InvalidOperationException("CurrencyCultureProvider is already initialized.");

        _userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
    }

    public static CultureInfo GetCultureFromUserPrimaryCurrency()
    {       
        var currencyCode = _userSession.CurrentUser.PrimaryCurrencyBalance.Currency;

        var cultureName = _defaultCurrencyCultures[currencyCode];

        return new CultureInfo(cultureName);
    }

    public static List<string> GetCurrenciesList()
    {
        return _defaultCurrencyCultures.Keys.ToList();
    }
}
