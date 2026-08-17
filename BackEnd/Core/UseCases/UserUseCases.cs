using Core.Models;
using Core.UseCases.Abstractions;
using Core.IGateways;
using Core.Models.Request;

namespace Core.UseCases;

public class UserUseCases : IUserUseCases
{
    private readonly IUserGateway _userGateway;
    private readonly IAddressGateaway _addressGateway;
    private readonly ILocalityGateaway _localiteGateway;

    public UserUseCases(IUserGateway userGateway,IAddressGateaway addressGateaway,ILocalityGateaway localityGateaway)
    {
        _userGateway = userGateway;
        _addressGateway=addressGateaway;
        _localiteGateway=localityGateaway;
    }

    public User AuthenticateAndGetUser(AuthenticationRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {            
            throw new ArgumentException("Username and password are required.", nameof(request));
        }

        var user = _userGateway.GetUserByUsername(request.Username);
        if (user == null)
        {
            throw new ArgumentException("Invalid username or password."); 
        }

        var hashedPassword = _userGateway.GetUserPasswordHash(request.Username); 
        if (string.IsNullOrEmpty(hashedPassword))
        {
            throw new InvalidOperationException("Could not retrieve password for user."); 
        }

        if (BCrypt.Net.BCrypt.Verify(request.Password, hashedPassword))
        {
            return user;
        }

        throw new ArgumentException("Invalid username or password.");
    }

    public IEnumerable<User> GetAllUsers()
    {
        var users = _userGateway.GetAllUsers();
        return users;
    }

    public void Register(RegisterRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("Invalid registration request");
        }

        if (request.Password != request.ConfirmPassword)
        {
            throw new ArgumentException("Passwords do not match");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        _userGateway.AddUser(request.Username, hashedPassword);
    }

    public ProfileRequest GetProfile(Guid userId)
    {
        var user = _userGateway.GetUserById(userId);
        if (user is null) throw new Exception("Utilisateur inexistant");
        var profil = new ProfileRequest
        {
            Username =user.Username,
            Nom = user.Nom,
            Prenom = user.Prenom
        };

        if (user.IdFacturation!=null)
        {
            var address = _addressGateway.GetAddressById(user.IdFacturation.Value);
            
            var localite = _localiteGateway.GetLocalityById(address.IdLocalite);
            

            profil.Facturation= new AddressRequest
            {
                Rue=address.Rue,
                Numero=address.Numero,
                Boite=address.Boite,
                localite = new LocalityRequest
                {
                    CodePostal=localite.CodePostal,
                    Ville=localite.Ville,
                    Province=localite.Province,
                }
            };
        }

        if (user.IdLivraison!=null)
        {
            var address = _addressGateway.GetAddressById(user.IdLivraison.Value);
            var localite = _localiteGateway.GetLocalityById(address.IdLocalite);

            profil.Livraison = new AddressRequest
            {
                Rue=address.Rue,
                Numero=address.Numero,
                Boite=address.Boite,
                localite = new LocalityRequest
                {
                    CodePostal=localite.CodePostal,
                    Ville=localite.Ville,
                    Province=localite.Province,
                }
            };
        }
        return profil;
    }
    public void UpdateLivraison(AddressRequest request,Guid userId)
    {
        var user = _userGateway.GetUserById(userId);
        var localiteLivraison = _localiteGateway.AddLocality(new Locality
                                                            {
                                                                CodePostal=request.localite.CodePostal,
                                                                Ville=request.localite.Ville,
                                                                Province=request.localite.Province
                                                            });
        var adresseLivraison = _addressGateway.AddAdress(new Address
        {
            Rue=request.Rue,
            Numero=request.Numero,
            Boite=request.Boite,
            IdLocalite=localiteLivraison
        });
        user.IdLivraison =adresseLivraison;  
    }
    public void UpdateFacturation(AddressRequest request,Guid userId)
    {
        var user = _userGateway.GetUserById(userId);
        var localiteFacturation = _localiteGateway.AddLocality(new Locality
                                                            {
                                                                CodePostal=request.localite.CodePostal,
                                                                Ville=request.localite.Ville,
                                                                Province=request.localite.Province
                                                            });
        var adresseFacturation = _addressGateway.AddAdress(new Address
        {
            Rue=request.Rue,
            Numero=request.Numero,
            Boite=request.Boite,
            IdLocalite=localiteFacturation
        });
        user.IdFacturation =adresseFacturation;  
    }
    

    
}