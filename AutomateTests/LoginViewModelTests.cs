using Automate.ViewModels;
using Xunit;

namespace AutomateTests
{
    public class LoginViewModelTests
    {
        // Test 1: Vérification de l'état initial de Username et Password
        [Fact]
        public void InitialProperties_ShouldBeNull()
        {
            // Arrange
            var loginViewModel = new LoginViewModel(null); // On ne passe pas de fenêtre ici car ce test ne dépend pas de la navigation

            // Assert: Vérifie que les propriétés Username et Password sont nulles au départ
            Assert.Null(loginViewModel.Username);
            Assert.Null(loginViewModel.Password);
        }

        // Test 2: Vérification que des erreurs sont ajoutées lorsque les champs sont vides
        [Fact]
        public void Validate_EmptyFields_ShouldAddErrors()
        {
            // Arrange
            var loginViewModel = new LoginViewModel(null); // Pas besoin de fenêtre pour ce test

            // Act: Essayer de valider avec des champs vides
            loginViewModel.ValidateProperty(nameof(loginViewModel.Username)); // Valider Username vide
            loginViewModel.ValidateProperty(nameof(loginViewModel.Password)); // Valider Password vide

            // Assert: Vérifie que des erreurs ont été ajoutées
            Assert.True(loginViewModel.HasErrors);
            Assert.Contains("Le nom d'utilisateur ne peut pas être vide.", loginViewModel.ErrorMessages);
            Assert.Contains("Le mot de passe ne peut pas être vide.", loginViewModel.ErrorMessages);
        }

    }
}
