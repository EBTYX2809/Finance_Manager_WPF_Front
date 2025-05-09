using AutoMapper;
using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_WPF_Front.Services.AuthServices;

public class AuthService
{
    private readonly FinanceApiClient _apiClient;
    private readonly ApiWrapper _apiWrapper;
    private readonly UserSession _userSession;    
    private readonly TokensManager _tokensManager;
    private readonly IMapper _mapper;

    public AuthService(FinanceApiClient apiClient, UserSession userSession, IMapper mapper, TokensManager tokensManager, ApiWrapper apiWrapper)
    {
        _apiClient = apiClient;
        _userSession = userSession;
        _mapper = mapper;
        _tokensManager = tokensManager;
        _apiWrapper = apiWrapper;
    }

    public async Task RegisterUser(string email, SecureString securePassword)
    {
        string password = null;
        var ptr = IntPtr.Zero;

        try
        {
            ptr = Marshal.SecureStringToBSTR(securePassword);
            password = Marshal.PtrToStringBSTR(ptr);

            var registerResponse = await _apiWrapper.ExecuteAsync(async () => 
            await _apiClient.RegisterAsync(new AuthDataDTO { Email = email, Password = password }));

            //if (registerResponse == null) throw new InvalidOperationException();

            _userSession.CurrentUser = _mapper.Map<UserModel>(registerResponse.UserDTO);
            _userSession.AccessToken = registerResponse.AccessJwtToken;
            _tokensManager.SaveRefreshToken(registerResponse.RefreshToken);
        }
        catch (Exception ex) { throw; }
        finally
        {
            if(ptr != IntPtr.Zero) Marshal.ZeroFreeBSTR(ptr);

            if (password != null) password = null;
        }                       
    }

    public async Task AuthUser(string email, SecureString securePassword)
    {

        string password = null;
        var ptr = IntPtr.Zero;

        try
        {
            ptr = Marshal.SecureStringToBSTR(securePassword);
            password = Marshal.PtrToStringBSTR(ptr);

            var registerResponse = await _apiWrapper.ExecuteAsync(async () =>
            await _apiClient.AuthenticateAsync(new AuthDataDTO { Email = email, Password = password }));

            //if (registerResponse == null) throw new InvalidOperationException();

            _userSession.CurrentUser = _mapper.Map<UserModel>(registerResponse.UserDTO);
            _userSession.AccessToken = registerResponse.AccessJwtToken;
            _tokensManager.SaveRefreshToken(registerResponse.RefreshToken);
        }
        catch (Exception ex) { throw; }
        finally
        {
            if (ptr != IntPtr.Zero) Marshal.ZeroFreeBSTR(ptr);

            if (password != null) password = null;
        }
    }

    public async Task LoginUserWithRefreshToken(string refreshToken)
    {
        var registerResponse = await _apiWrapper.ExecuteAsync(async () =>
        await _apiClient.RefreshTokenAsync(refreshToken));

        //if (registerResponse == null) throw new InvalidOperationException(); 

        _userSession.CurrentUser = _mapper.Map<UserModel>(registerResponse.UserDTO);
        _userSession.AccessToken = registerResponse.AccessJwtToken;
        _tokensManager.SaveRefreshToken(registerResponse.RefreshToken);
    }
}
