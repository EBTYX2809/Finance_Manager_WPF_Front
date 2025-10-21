<div align="center">
  <h1>Application for managing your finances by tracking spends and incomes.</h1>
  <h3>General description</h1> 
  <h4>It's just frontend layer for my finance manager backend server. Developed on WPF and it allows users to view, edit, and analyze their finances by a simple, but good enough interface.</h3>
</div>

### Technical details:
- Frontend architecture is MVVM with OnPropertyChaned live updates.
- For managing all services implemented Dependency Injection library.
- Layer for interactions with API endpoints created by nswag code generator.
- Implemented authentication with JWT tokens with refresh system.
- Automapper used for mapping DTOs from backend to OnPropetryChanged models on frontend.
- Logging added by Serilog library.
- Created autoconverter for currencies in user balance.
- Added something like Global Exception Handler with Command pattern.
- Analytics diagram drawing by LiveChartsCore library.
- CI/CD pipeline implemented for autoupdating application archive on server. Autoupdating every downloaded client will be soon.

<div align="center">
  <h1> Screenshots section: </h1>
  <h3> Authentication window </h3>
  <img src="https://github.com/EBTYX2809/Finance_Manager_WPF_Front/blob/master/Interface_screenshots/auth_placeholders.jpg">
  <img src="https://github.com/EBTYX2809/Finance_Manager_WPF_Front/blob/master/Interface_screenshots/auth_data.jpg">

  <h3> Transactions page </h3>
  <img src="https://github.com/EBTYX2809/Finance_Manager_WPF_Front/blob/master/Interface_screenshots/transaction_page.jpg">
  <img src="https://github.com/EBTYX2809/Finance_Manager_WPF_Front/blob/master/Interface_screenshots/transaction_window.jpg">

  <h3> Savings page </h3>
  <img src="https://github.com/EBTYX2809/Finance_Manager_WPF_Front/blob/master/Interface_screenshots/savings_page.jpg">
  <img src="https://github.com/EBTYX2809/Finance_Manager_WPF_Front/blob/master/Interface_screenshots/saving_window.jpg">

  <h3> Analytics page </h3>
  <img src="https://github.com/EBTYX2809/Finance_Manager_WPF_Front/blob/master/Interface_screenshots/analytics.jpg">
  
  <h3> Settings page </h3>
  <img src="https://github.com/EBTYX2809/Finance_Manager_WPF_Front/blob/master/Interface_screenshots/settings.jpg">
</div>
