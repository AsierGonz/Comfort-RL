# 🤖 Comfort-RL: Reinforcement Learning for Human-Centered Robotics

**Comfort-RL** is an interactive simulation environment built in Unity and powered by ML-Agents, designed for training robotic agents that explicitly consider **human comfort** in their behavior. It enables the development and evaluation of comfort-aware policies using reinforcement learning in both desktop and VR contexts.

> 🆕 Full source code and PreRelease executable now available  
> 🔗 Related paper: [IEEE Xplore - Comfort-Aware RL](https://ieeexplore.ieee.org/document/10658649)

<img src="utils_readme/gif_1.gif" alt="RoboticGIF" width="400"/>

---

## 🧠 Key Features

- 🧩 Unity-based simulation using **ML-Agents Toolkit**
- 💬 Customizable **comfort-aware reward functions**
- 🧪 Includes both **simple and complex robotic tasks**
- 🕶️ Integrated **VR environment** for immersive agent testing
- 🤝 Easily extendable and compatible with external ML libraries

> ML-Agents Toolkit: [Unity-Technologies/ml-agents](https://github.com/Unity-Technologies/ml-agents)

---

## 🚀 What's Included

- ✅ Full Unity source code for customization and training
- ✅ PreRelease `.exe` to explore the environment without building
- ✅ Multiple robotic task scenarios (pick & place, proximity adaptation, etc.)
- ✅ Early-access **VR testing environment** (`VR_Scene/`) for drag-and-drop model evaluation
- ⚠️ Codebase under active development — expect **code cleanup and full commenting** in future commits

---

## 📂 Folder Overview

```
Comfort-RL/
├── Comfort-RL_Source_code/     # Unity project for training and simulation
├── pre_realease_test_1_0/      # Windows executable build for testing (PreRelease)
├── VR-Robotic-SetUp/           # VR scene for immersive evaluation with trained agents
└──  utils_readme/               # Images and media for documentation
```

---

## 🔜 Future Updates

Ongoing work includes:

- 📝 Complete documentation of reward configuration and training setup  
- 👤 Adaptive personalization of comfort functions  
- 🧠 Export/import pipelines for `.nn` files  
- 🕶️ Expansion of the VR interface with better interaction tools

---

## 📖 Reference

If you use this environment, please cite the related paper:

```bibtex
@article{GonzalezSantocildes2024,
  title = {Adaptive Robot Behavior Based on Human Comfort Using Reinforcement Learning},
  volume = {12},
  ISSN = {2169-3536},
  url = {http://dx.doi.org/10.1109/ACCESS.2024.3451663},
  DOI = {10.1109/access.2024.3451663},
  journal = {IEEE Access},
  publisher = {Institute of Electrical and Electronics Engineers (IEEE)},
  author = {Gonzalez-Santocildes, Asier and Vazquez, Juan-Ignacio and Eguiluz, Andoni},
  year = {2024},
  pages = {122289–122299}
}
```

---

## 🛠 Feedback & Contributions

This project is open to community feedback and collaboration.  
Feel free to open an issue or pull request in the [GitHub repository](https://github.com/AsierGonz/Comfort-RL).

---
