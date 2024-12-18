using Automate.Models;
using Automate.ViewModels;
using Moq;
using System.Windows.Media;
using Xunit;

namespace AutomateTests
{
    public class SerreViewModelTests
    {
        [Fact]
        public void ToggleSystem_Fenetres_ShouldToggleState()
        {
            // Arrange
            var serreViewModel = new SerreViewModel();

            // Act: Bascule l'état des fenêtres
            serreViewModel.ToggleSystem("Fenetres");

            // Assert: Vérifie que l'état est passé à "Activé" et la couleur à "Green"
            Assert.Equal("Activé", serreViewModel.FenetresState);
            Assert.Equal(Brushes.Green, serreViewModel.FenetresColor);

            // Act: Re-bascule l'état des fenêtres
            serreViewModel.ToggleSystem("Fenetres");

            // Assert: Vérifie que l'état est revenu à "Désactivé" et la couleur à "Gray"
            Assert.Equal("Désactivé", serreViewModel.FenetresState);
            Assert.Equal(Brushes.Gray, serreViewModel.FenetresColor);
        }

        [Fact]
        public void InitialClimateSuggestions_ShouldBeNA()
        {
            // Arrange
            var serreViewModel = new SerreViewModel();

            // Act: Vérifie les suggestions climatiques initiales
            var tempSuggestion = serreViewModel.TemperatureSuggestion;
            var humiditySuggestion = serreViewModel.HumiditySuggestion;
            var luminositySuggestion = serreViewModel.LuminositySuggestion;

            // Assert: Vérifie que toutes les suggestions initiales sont "N/A"
            Assert.Equal("N/A", tempSuggestion);
            Assert.Equal("N/A", humiditySuggestion);
            Assert.Equal("N/A", luminositySuggestion);
        }

        [Fact]
        public void IsReading_ShouldBeFalseInitially()
        {
            // Arrange
            var serreViewModel = new SerreViewModel();

            // Assert: Vérifie que IsReading est initialisé à false
            Assert.False(serreViewModel.IsReading);
        }

        [Fact]
        public void ToggleSystem_Chauffage_ShouldToggleState()
        {
            // Arrange
            var serreViewModel = new SerreViewModel();

            // Act: Bascule l'état du chauffage
            serreViewModel.ToggleSystem("Chauffage");

            // Assert: Vérifie que l'état est passé à "Activé" et la couleur à "Green"
            Assert.Equal("Activé", serreViewModel.ChauffageState);
            Assert.Equal(Brushes.Green, serreViewModel.ChauffageColor);

            // Act: Re-bascule l'état du chauffage
            serreViewModel.ToggleSystem("Chauffage");

            // Assert: Vérifie que l'état est revenu à "Désactivé" et la couleur à "Gray"
            Assert.Equal("Désactivé", serreViewModel.ChauffageState);
            Assert.Equal(Brushes.Gray, serreViewModel.ChauffageColor);
        }

        [Fact]
        public void ToggleSystem_Ventilateur_ShouldToggleState()
        {
            // Arrange
            var serreViewModel = new SerreViewModel();

            // Act: Bascule l'état du ventilateur
            serreViewModel.ToggleSystem("Ventilateur");

            // Assert: Vérifie que l'état est passé à "Activé" et la couleur à "Green"
            Assert.Equal("Activé", serreViewModel.VentilateurState);
            Assert.Equal(Brushes.Green, serreViewModel.VentilateurColor);

            // Act: Re-bascule l'état du ventilateur
            serreViewModel.ToggleSystem("Ventilateur");

            // Assert: Vérifie que l'état est revenu à "Désactivé" et la couleur à "Gray"
            Assert.Equal("Désactivé", serreViewModel.VentilateurState);
            Assert.Equal(Brushes.Gray, serreViewModel.VentilateurColor);
        }
    }
}
