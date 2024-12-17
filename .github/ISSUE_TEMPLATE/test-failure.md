---
name: Tests échoués
about: Crée une issue pour les tests qui échouent
title: "❗ Échec des tests CI"
labels: bug, ci
body:
  - type: markdown
    attributes:
      value: |
        ### Résumé des tests échoués
        Les tests CI ont échoué lors du dernier pipeline d'intégration continue.

        **Détails des erreurs :**
        ```
        ${{ steps.test.outputs.errors }}
        ```

        **Étape à reproduire :**
        - Exécuter `dotnet test`
  - type: textarea
    id: log
    attributes:
      label: Logs des tests
      description: Copiez et collez ici les logs des tests échoués.
