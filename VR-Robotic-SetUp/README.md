# 🕶️ Comfort-RL VR Testing Environment

This folder contains the **VR scene** of the Comfort-RL project. It allows you to **load and test trained agents** in a fully immersive environment using Unity and ML-Agents.

---

## 🎯 Purpose

This VR setup is designed to:

- Test **comfort-aware agents** trained via reinforcement learning in a virtual reality setting
- Allow intuitive inspection of robot behaviors from a first-person perspective
- Provide a quick way to **drag-and-drop `.nn` models** and evaluate their comfort-driven motion policies

---

## 📂 Contents

- ✅ A working **Unity VR scene** using OpenXR (Oculus / SteamVR compatible)
- ✅ Drop zone for ML-Agent models (`.nn`) — simply place them in `Assets/ML-Agents/Models/`
- ✅ Basic interaction and teleportation mechanics for movement and observation
- ✅ Preconfigured settings aligned with the Comfort-RL training environment

---

## 🛠 Development Status

- ⚠️ **Code cleaning is in progress**: expect updates in variable names, folder organization, and comments.
- 🔧 Requires Unity 2021.3+, ML-Agents Toolkit, and OpenXR plugin
- 🧪 Tested with trained SAC and PPO models from the main Comfort-RL setup

---

## 🚀 How to Use

1. Open this Unity scene with Unity 2021.3+.
2. Drop your trained `.nn` file into:  
   `Assets/ML-Agents/Models/`
3. Select the Agent GameObject and assign the new model in the Behavior Parameters component.
4. Enter Play mode with your VR headset connected.
5. Observe and interact with the robot to evaluate comfort-based behaviors.

---

## 📬 Feedback

Issues, suggestions, or improvements are welcome via the main repo:  
🔗 [github.com/AsierGonz/Comfort-RL](https://github.com/AsierGonz/Comfort-RL)
