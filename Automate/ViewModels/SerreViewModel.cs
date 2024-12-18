using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Automate.Utils;

namespace Automate.ViewModels
{
    public class SerreViewModel : INotifyPropertyChanged
    {
        // Champs privés pour les états et couleurs
        private string _fenetresState = "Désactivé";
        private Brush _fenetresColor = Brushes.Gray;

        private string _chauffageState = "Désactivé";
        private Brush _chauffageColor = Brushes.Gray;

        private string _ventilateurState = "Désactivé";
        private Brush _ventilateurColor = Brushes.Gray;

        private string _arrosageState = "Désactivé";
        private Brush _arrosageColor = Brushes.Gray;

        private string _lumiereState = "Désactivé";
        private Brush _lumiereColor = Brushes.Gray;

        // Propriétés publiques pour la liaison de données
        public string FenetresState
        {
            get => _fenetresState;
            set
            {
                _fenetresState = value;
                OnPropertyChanged();
            }
        }

        public Brush FenetresColor
        {
            get => _fenetresColor;
            set
            {
                _fenetresColor = value;
                OnPropertyChanged();
            }
        }

        public string ChauffageState
        {
            get => _chauffageState;
            set
            {
                _chauffageState = value;
                OnPropertyChanged();
            }
        }

        public Brush ChauffageColor
        {
            get => _chauffageColor;
            set
            {
                _chauffageColor = value;
                OnPropertyChanged();
            }
        }

        public string VentilateurState
        {
            get => _ventilateurState;
            set
            {
                _ventilateurState = value;
                OnPropertyChanged();
            }
        }

        public Brush VentilateurColor
        {
            get => _ventilateurColor;
            set
            {
                _ventilateurColor = value;
                OnPropertyChanged();
            }
        }

        public string ArrosageState
        {
            get => _arrosageState;
            set
            {
                _arrosageState = value;
                OnPropertyChanged();
            }
        }

        public Brush ArrosageColor
        {
            get => _arrosageColor;
            set
            {
                _arrosageColor = value;
                OnPropertyChanged();
            }
        }

        public string LumiereState
        {
            get => _lumiereState;
            set
            {
                _lumiereState = value;
                OnPropertyChanged();
            }
        }

        public Brush LumiereColor
        {
            get => _lumiereColor;
            set
            {
                _lumiereColor = value;
                OnPropertyChanged();
            }
        }

        // Propriétés liées aux états climatiques
        public string TemperatureSuggestion { get; set; }
        public string HumiditySuggestion { get; set; }
        public string LuminositySuggestion { get; set; }

        // Chemin vers le fichier CSV
        // Chemin vers le fichier CSV
        private readonly string _csvPath = "C:\\Users\\Abdelbrahim Tounao K\\Source\\Repos\\Automate-Pt.2\\Utils\\TempData.csv";


        // Timer pour lire les données toutes les 10 secondes
        private Timer _timer;
        private bool _isReading;

        private List<string> _csvLines; // Stocke les lignes du fichier CSV
        private int _currentLineIndex;  // Index pour suivre la lecture des lignes

        // Commandes pour les systèmes
        public ICommand ToggleFenetresCommand { get; }
        public ICommand ToggleChauffageCommand { get; }
        public ICommand ToggleVentilateurCommand { get; }
        public ICommand ToggleArrosageCommand { get; }
        public ICommand ToggleLumiereCommand { get; }

        public ICommand StartReadingCommand { get; }
        public ICommand StopReadingCommand { get; }
        public bool IsReading { get; set; }

        // Constructeur
        public SerreViewModel()
        {
            ToggleFenetresCommand = new RelayCommand(() => ToggleSystem("Fenetres"));
            ToggleChauffageCommand = new RelayCommand(() => ToggleSystem("Chauffage"));
            ToggleVentilateurCommand = new RelayCommand(() => ToggleSystem("Ventilateur"));
            ToggleArrosageCommand = new RelayCommand(() => ToggleSystem("Arrosage"));
            ToggleLumiereCommand = new RelayCommand(() => ToggleSystem("Lumiere"));


            StartReadingCommand = new RelayCommand(StartReadingData);
            StopReadingCommand = new RelayCommand(StopReadingData);

            TemperatureSuggestion = "N/A";
            HumiditySuggestion = "N/A";
            LuminositySuggestion = "N/A";
        }

        // Méthode pour basculer l'état d'un système
        public void ToggleSystem(string systemName)
        {
            switch (systemName)
            {
                case "Fenetres":
                    FenetresState = (FenetresState == "Activé") ? "Désactivé" : "Activé";
                    FenetresColor = (FenetresState == "Activé") ? Brushes.Green : Brushes.Gray;
                    break;

                case "Chauffage":
                    ChauffageState = (ChauffageState == "Activé") ? "Désactivé" : "Activé";
                    ChauffageColor = (ChauffageState == "Activé") ? Brushes.Green : Brushes.Gray;
                    break;

                case "Ventilateur":
                    VentilateurState = (VentilateurState == "Activé") ? "Désactivé" : "Activé";
                    VentilateurColor = (VentilateurState == "Activé") ? Brushes.Green : Brushes.Gray;
                    break;

                case "Arrosage":
                    ArrosageState = (ArrosageState == "Activé") ? "Désactivé" : "Activé";
                    ArrosageColor = (ArrosageState == "Activé") ? Brushes.Green : Brushes.Gray;
                    break;

                case "Lumiere":
                    LumiereState = (LumiereState == "Activé") ? "Désactivé" : "Activé";
                    LumiereColor = (LumiereState == "Activé") ? Brushes.Green : Brushes.Gray;
                    break;

                default:
                    throw new ArgumentException("Système inconnu", nameof(systemName));
            }
        }


        /// <summary>
        /// Démarre la lecture des données météo depuis tempData.csv.
        /// </summary>
        public void StartReadingData()
        {
            try
            {
                if (_isReading) return;

                if (!File.Exists(_csvPath))
                {
                    MessageBox.Show($"Fichier introuvable : {_csvPath}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _csvLines = new List<string>(File.ReadAllLines(_csvPath)); // Charge les lignes
                _currentLineIndex = 1; // Ignore l'en-tête

                if (_csvLines.Count <= 1)
                {
                    MessageBox.Show("Aucune donnée disponible dans le fichier tempData.csv.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _isReading = true;
                _timer = new Timer(ReadNextLine, null, 0, 10000); // Timer : 0ms au début, puis toutes les 10 secondes
                MessageBox.Show("Lecture des données météo démarrée.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la lecture des données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Arrête la lecture des données.
        /// </summary>
        public void StopReadingData()
        {
            if (_isReading && _timer != null)
            {
                _timer.Dispose();
                _isReading = false;
                MessageBox.Show("Lecture des données météo arrêtée.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        /// <summary>
        /// Lit la prochaine ligne de tempData.csv et met à jour les suggestions climatiques.
        /// </summary>
        private void ReadNextLine(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    // Assurez-vous que _csvLines est initialisé
                    if (_csvLines == null || _csvLines.Count == 0)
                    {
                        MessageBox.Show("Les données CSV sont vides ou non initialisées.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        StopReadingData(); // Arrêter la lecture si nécessaire
                        return;
                    }

                    if (_currentLineIndex >= _csvLines.Count)
                    {
                        StopReadingData();
                        MessageBox.Show("Toutes les données ont été lues.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    var line = _csvLines[_currentLineIndex];
                    var data = line.Split(',');

                    if (data.Length == 4)
                    {
                        UpdateClimateSuggestions(data);
                    }

                    _currentLineIndex++;
                }
                catch (Exception ex)
                {
                    StopReadingData();
                    MessageBox.Show($"Erreur lors de la lecture de la ligne : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }


        /// <summary>
        /// Met à jour les suggestions climatiques en fonction des données lues.
        /// </summary>
        /// <param name="data">Tableau contenant les valeurs de la ligne CSV.</param>
        public void UpdateClimateSuggestions(string[] data)
        {
            if (double.TryParse(data[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double temperature))
            {
                TemperatureSuggestion = temperature > 30
                    ? "Température élevée, activez le ventilateur."
                    : temperature < 18
                        ? "Température basse, activez le chauffage."
                        : "Température optimale.";
            }

            if (double.TryParse(data[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double humidity))
            {
                HumiditySuggestion = humidity < 40
                    ? "Humidité faible, activez l'arrosage."
                    : humidity > 70
                        ? "Humidité élevée, réduisez l'arrosage."
                        : "Humidité optimale.";
            }

            if (double.TryParse(data[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double luminosity))
            {
                LuminositySuggestion = luminosity < 300
                    ? "Luminosité faible, activez les lumières."
                    : "Luminosité optimale.";
            }

            // Déclenche la notification pour la vue
            OnPropertyChanged(nameof(TemperatureSuggestion));
            OnPropertyChanged(nameof(HumiditySuggestion));
            OnPropertyChanged(nameof(LuminositySuggestion));
        }

        // Implémentation de l'interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}