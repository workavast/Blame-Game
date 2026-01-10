# Exostructure

A dark, top-down sci-fi survival-like game inspired by Blame! and Vampire Survivors.

You land on the surface of the space megastructure.

Your task is to distract the security systems and survive while another squad explores its inner workings.

You operate a combat robotic spider, surrounded by endless waves of hostile drones and automated systems.

Control:
* Movemant: **WASD** or arrows
* Open menu: **ESC**
* Swap bestiary page: **Q\E**

## Tech Stack

[![Static Badge](https://img.shields.io/badge/Unity%206.1-000000?logo=unity)](https://unity.com)
[![Static Badge](https://img.shields.io/badge/Entities-black?logo=unity&label=DOTS&labelColor=black&color=gray)](https://docs.unity3d.com/Packages/com.unity.entities@1.3/manual/index.html)
![Static Badge](https://img.shields.io/badge/URP-000000?logo=unity)
[![Static Badge](https://img.shields.io/badge/Cinemachine-000000?logo=unity)](https://unity.com/unity/features/editor/art-and-design/cinemachine)
[![Static Badge](https://img.shields.io/badge/Input%20System-000000?logo=unity)](https://docs.unity3d.com/Packages/com.unity.inputsystem@latest)
[![Static Badge](https://img.shields.io/badge/Localization-000000?logo=unity)](https://docs.unity3d.com/Packages/com.unity.localization@1.5/manual/index.html)
[![Static Badge](https://img.shields.io/badge/Addressables-000000?logo=unity)](https://docs.unity3d.com/Packages/com.unity.addressables@2.7/manual/index.html)

[![Static Badge](https://img.shields.io/badge/Zenject-green)](https://github.com/modesttree/Zenject)
[![Static Badge](https://img.shields.io/badge/Custom%20Toolbar-gray)](https://github.com/smkplus/CustomToolbar)
[![Static Badge](https://img.shields.io/badge/Mesh%20Combiner-gray)](https://github.com/dawid-t/Mesh-Combiner)

## Demonstration

[![Static Badge](https://img.shields.io/badge/link-white?style=flat&logo=itchdotio&label=Itch.io&labelColor=white&color=gray)](https://avastrad.itch.io/exostructure)

<details><summary><h3>Gifs</h3></summary>
  
  ![Gameplay-gif](https://github.com/user-attachments/assets/302c9f62-41db-4f1e-bbd7-76cd8f37fafe)
  
  ![Perks Cards-gif](https://github.com/user-attachments/assets/4fd31d72-4210-417f-b139-6fe02508936c)
</details>

<details><summary><h3>Screenshots</h3></summary>
  
  <img width="800" height="450" alt="Gameplay" src="https://github.com/user-attachments/assets/c5c46d6d-1f7a-4a60-a938-61994e2fa297" />
  <p></p>
  <img width="800" height="450" alt="Active Perks Grid" src="https://github.com/user-attachments/assets/342f3fbf-b21c-4731-a0ec-d4baba06b207" />
  <p></p>
  <img width="800" height="450" alt="Bestiary" src="https://github.com/user-attachments/assets/13277fac-935f-4765-8854-92ceed1a2591" />
</details>

<details><summary><h3>Tech. Details</h3></summary>

### Bootstraps
Each scene uses a Bootstrap system. This consists of a set of game objects with specific components bootstraps. Each bootstrap executes asynchronously. Execution occurs sequentially based on the hierarchy of nested objects in the scene. The order of initialization can be changed by simply changing the order of the objects in the hierarchy.

<img width="860" height="450" alt="Bootstraps Class Diagram With Example" src="https://github.com/user-attachments/assets/f4853eca-bb25-42c1-a1eb-b810a9c5df01" />

### Settings
The settings system is built on MVVM with a centralized repository. Adding new settings is simplified thanks to code generation: simply specify the setting name, and all the necessary classes are automatically created without modifying existing code. State is saved using Newtonsoft.Json as an array, preserving element type information and validating it during deserialization. This allows for seamless changes to existing code without risking critical errors during backward compatibility.

<img width="1103" height="549" alt="Settings Class Diagram" src="https://github.com/user-attachments/assets/d2398128-6d54-4b1f-8e3a-19a850c9ff7c" />

### ECS Views
Because the project uses shaders that are not supported by the Entities Graphics system, a Views system has been implemented for rendering in the main scene. This system loads and initializes entity views asynchronously. After an entity is destroyed, its view is notified and should automatically unload itself. The main view (EntityView) can have multiple child views (IEntityViewElement).

<img width="1092" height="333" alt="Ecs Views Class Diagram" src="https://github.com/user-attachments/assets/bcb78a8e-f353-40ac-8e1a-7ffc9d018650" />

### SFX
The SFX system uses ECS views, but sound files are loaded separately from the views. System supports object pooling.

<img width="1145" height="429" alt="SFX Class Diagram" src="https://github.com/user-attachments/assets/a1993b5c-11e1-4dd7-afad-d6ceb5bc0ac8" />


</details>
