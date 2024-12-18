//using Automate.Utils;
//using Automate.Views;
//using Automate.ViewModels;
//using Xunit;
//using System;
//using System.Windows;
//using Automate.Models;

//namespace AutomateTests
//{
//    public class LoginViewModelTests
//    {
//        private readonly LoginViewModel _viewModel;

//        public LoginViewModelTests()
//        {
//            // Créer le ViewModel avec une fenêtre fictive
//            _viewModel = new LoginViewModel(new Window());
//        }

//        [Fact]
//        public void Username_ShouldBeValid_WhenSetToNonEmptyValue()
//        {
//            // Arrange
//            var username = "admin";

//            // Act
//            _viewModel.Username = username;

//            // Assert
//            Assert.Equal(username, _viewModel.Username);
//            Assert.False(_viewModel.HasErrors);
//        }

//        [Fact]
//        public void Username_ShouldBeInvalid_WhenSetToEmptyValue()
//        {
//            // Arrange
//            var username = "";

//            // Act
//            _viewModel.Username = username;

//            // Assert
//            Assert.True(_viewModel.HasErrors);
//            Assert.Contains("Le nom d'utilisateur ne peut pas être vide.", _viewModel.ErrorMessages);
//        }

//        [Fact]
//        public void Password_ShouldBeValid_WhenSetToNonEmptyValue()
//        {
//            // Arrange
//            var password = "admin123";

//            // Act
//            _viewModel.Password = password;

//            // Assert
//            Assert.Equal(password, _viewModel.Password);
//            Assert.False(_viewModel.HasErrors);
//        }

//        [Fact]
//        public void Password_ShouldBeInvalid_WhenSetToEmptyValue()
//        {
//            // Arrange
//            var password = "";

//            // Act
//            _viewModel.Password = password;

//            // Assert
//            Assert.True(_viewModel.HasErrors);
//            Assert.Contains("Le mot de passe ne peut pas être vide.", _viewModel.ErrorMessages);
//        }

//        [Fact]
//        public void Authenticate_ShouldFail_WhenUserNotFound()
//        {
//            // Arrange
//            _viewModel.Username = "wrongUser";
//            _viewModel.Password = "wrongPassword";

//            // Ici, nous utilisons une méthode simple de comparaison, car MongoDBService est réel
//            // Simuler une authentification en utilisant un utilisateur fictif ou une fausse méthode
//            var user = AuthenticateFake(_viewModel.Username, _viewModel.Password);

//            // Act
//            _viewModel.Authenticate();

//            // Assert
//            Assert.Contains("Nom d'utilisateur ou mot de passe invalide.", _viewModel.ErrorMessages);
//        }

//        [Fact]
//        public void Authenticate_ShouldSucceed_WhenUserIsFound()
//        {
//            // Arrange
//            _viewModel.Username = "admin";
//            _viewModel.Password = "admin123";

//            // Simuler l'authentification avec un utilisateur valide
//            var user = AuthenticateFake(_viewModel.Username, _viewModel.Password);

//            // Act
//            _viewModel.Authenticate();

//            // Assert
//            // Si l'utilisateur existe, la navigation doit se produire.
//            Assert.False(_viewModel.HasErrors);
//        }

//        // Méthode pour simuler l'authentification
//        private UserModel AuthenticateFake(string username, string password)
//        {
//            // Cette méthode simule une authentification en renvoyant un utilisateur fictif
//            if (username == "admin" && password == "admin123")
//            {
//                return new UserModel { Username = username, Password = password, Role = "Administrator" };
//            }
//            return null;
//        }
//    }
//}
