# VR Escape Game

Escape game en réalité virtuelle développé sous Unity dans le cadre de la 3e année de **BUT Informatique** (IUT Clermont Auvergne, 2024-2025). Le joueur progresse de salle en salle en résolvant des énigmes physiques : chaque porte ne s'ouvre que lorsque toutes les conditions de la salle sont remplies.

> *English summary: a room-based VR escape game built with Unity 6 and the XR Interaction Toolkit for the HTC Vive, as a team project during the final year of my Computer Science degree. I was responsible for the third room, a laser puzzle.*

---

## Contexte

| | |
|---|---|
| **Type** | Projet de groupe universitaire |
| **Durée** | Année universitaire 2024-2025 |
| **Casque cible** | HTC Vive (OpenXR / SteamVR) |
| **Ma contribution** | Conception et développement de la **Salle 3 : la salle des lasers** |

---

## Gameplay

- **Déplacement** par téléportation, **interaction** par saisie d'objets aux manettes (XR Interaction Toolkit).
- **Salles 1 et 2 — formes et réceptacles** : le joueur doit trouver des objets (cube, pyramide, sphère) et les déposer sur le réceptacle correspondant. Un objet correct est aimanté à sa position et verrouillé. Quand tous les réceptacles d'une salle sont complétés, la porte s'ouvre.
- **Salle 3 — lasers** : des canons émettent des faisceaux qui frappent des récepteurs. Le joueur doit **bloquer chaque faisceau à l'aide de caches** ; une fois tous les lasers interrompus, la porte s'ouvre.
- **Fin de partie** : écran avec les boutons *Rejouer* et *Quitter*.

---

## Ma contribution : la salle des lasers

J'ai pris en charge l'intégralité de la troisième salle, de la mise en place de la pièce à sa logique de jeu.

### Logique de détection
Je suis parti de l'asset **LaserMachine** (Lightbug), qui ne gère que l'affichage des faisceaux, et je l'ai étendu dans [`LaserMachine.cs`](Assets/Salle3/LaserMachine/Core/Scripts/LaserMachine.cs) pour en faire une mécanique d'énigme :

- À chaque frame, un `Physics.Linecast` part du canon. Si l'objet touché porte le tag **`Recepteur`**, le laser est considéré comme « actif » sur sa cible.
- Le laser est relié à la **porte** de la salle via le même composant [`Door`](Assets/Scripte/Door.cs) que les salles 1 et 2. Un récepteur est validé lorsque **son faisceau est interrompu** par un cache, et invalidé s'il est de nouveau touché. La porte compte les récepteurs validés et s'ouvre ou se referme en conséquence.
- Ce branchement sur le système de portes commun permet à la salle de s'insérer dans le jeu sans code spécifique côté porte.

### Rendu et retour au joueur
- **Son d'impact spatialisé** : une `AudioSource` 3D en boucle est créée à la volée et **suit le point d'impact** du laser, pour que le joueur localise les faisceaux à l'oreille.
- **Étincelles** au point d'impact, agrandies et orientées selon la normale de la surface touchée.
- Matériaux dédiés pour les canons, les faisceaux et les récepteurs.

### Assets de la salle
- Prefab de la pièce (`Salle3.prefab`), du laser (`LeLaser.prefab` et sa configuration `ZeLaser.asset`), des récepteurs (`BaseRecepteur.prefab`) et des caches (`CacheLaser.prefab`).

---

## Stack technique

| | |
|---|---|
| **Moteur** | Unity **6000.0.25f1** (Unity 6) |
| **Rendu** | Universal Render Pipeline 17 |
| **XR** | XR Interaction Toolkit 3.0.7, OpenXR 1.13.1, XR Hands 1.5.0, Input System 1.11.2 |
| **Langage** | C# |

---

## Lancer le projet

1. Installer **Unity 6000.0.25f1** via Unity Hub.
2. Cloner le dépôt, puis l'ouvrir depuis Unity Hub (*Add project from disk*). Le premier import régénère le dossier `Library/` et peut prendre plusieurs minutes.
3. Brancher le casque HTC Vive et lancer **SteamVR**.
4. Ouvrir la scène **`Assets/Scenes/SceneGame.unity`** (scène de jeu complète), puis lancer le mode Play.

---

## Structure

```
Assets/
├── Scripte/        Scripts de gameplay (portes, réceptacles, objets, GameManager)
├── Salle3/         Salle des lasers : prefabs, matériaux, récepteurs, asset LaserMachine modifié
├── Prefab/         Salles, portes, objets et réceptacles réutilisables
├── Animation/      Animations d'ouverture / fermeture des portes
├── Scenes/         SceneGame (jeu complet), scènes de test des mécaniques
└── *.fbx           Modèles 3D des salles et des éléments de décor
```

---

## Limites connues et pistes d'amélioration

Avec le recul, voici ce que je retravaillerais :

- **Encapsulation** : passer les champs publics en `[SerializeField] private` et remplacer les triggers d'animation en chaînes (`"open"`, `"close"`) par des `Animator.StringToHash`.
- **Robustesse** : vérifier les composants récupérés par `GetComponent` avant usage et gérer le retrait d'un objet de son réceptacle.
- **Découplage** : remplacer les références directes laser → porte par des `UnityEvent` ou des événements C#, pour qu'un récepteur ne dépende pas d'un type de porte précis.
- **Laser** : extraire la logique d'énigme de l'asset tiers dans un composant dédié, pour pouvoir mettre l'asset à jour sans perdre les modifications.
- **Dépôt** : versionner les binaires lourds (textures, vidéos, lightmaps) avec **Git LFS** et ajouter `SceneGame` aux *Build Settings*.

---

## Crédits

Projet réalisé en équipe dans le cadre du BUT Informatique de l'IUT Clermont Auvergne.

Assets tiers utilisés :
- **XR Interaction Toolkit** — samples et VR Template (Unity)
- **TextMesh Pro** (Unity)
- **LaserMachine** — Lightbug (modifié pour la salle des lasers)
- **Low Poly Furniture** et **Laser Weapons Sound Pack** — Unity Asset Store
