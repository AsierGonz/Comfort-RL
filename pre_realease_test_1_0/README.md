# Pre-Release Training Environment

This folder contains the pre-release version of the training environment for our ML-Agents project. This setup is intended to provide a foundational training configuration that can be easily launched using the `mlagents-learn` command, as described in Unity's [ML-Agents documentation](https://github.com/Unity-Technologies/ml-agents).

## Getting Started

To initiate training, ensure you have the [ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents) installed and configured. For training, simply run the following command:


`mlagents-learn trainer_config_SAC.yaml --run-id=<your_run_id> --env=PreRelease1_0.exe`


Replace `your_run_id` with a unique identifier for this training session. The configuration file (`trainer_config_SAC.yaml`) can be adjusted according to your specific training requirements, including hyperparameters, reward signals, and more.

### Customizing the Robotic Environment

Within the `config` folder, there is a JSON file that allows you to modify parameters specific to the robotic environment. Adjusting these settings lets you customize various aspects of the environment, such as robot behavior and environmental conditions, without needing to alter the core codebase. Go to the config folder to see the different configuration explanation. 

### Prerequisites

- Unity ML-Agents Toolkit installed
- A compatible version of Unity for the environment
- Python 3.x with the required dependencies as outlined in the ML-Agents documentation

### Future Release

In the final release, a comprehensive repository will be provided, including the complete source code for the environment. This will enable greater customization, simplify modifications to the environment, and provide clearer insights into the underlying structure and functionality. Feel free to reach out if you need any support before the final release. 

Happy training!