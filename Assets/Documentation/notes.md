# Docs Notes

- Audio
- Character
  - Animations
  - Sprites
- Documentation
- Editor
- Environment
  - Sprites
  - Tiles
- Prefabs
- Scenes
- Scripts
  - Core
  - Gameplay
  - Mechanics
  - Model
  - UI
  - View
- TextMesh Pro
- Tiles

Packages

TextMesh Pro is used as a replacement to the default Unity UI Text because it has much finer control over display, font type, text, etc. Just a more feature-rich package. Can keep.

Cinemachine is used to control the camera in the platformer. The main camera has a CinemachineBrain component which uses the CM vcam1 Transform to position. CM vcam1 has a CinemachineVirtualCamera component which tracks the Player Transform using some specific tracking stuff. There's a bit in the body (Framing Transposer) which has a deadzone, and compares with the Transform to move the camera. This could be useful in a 2D platformer, however this is less useful depending on the genre that I want to persue. Cinemachine might not be necessary for my other project.
