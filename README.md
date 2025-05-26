# UnityNavigationEnvs
Some goal-conditioned environments in unity.


## Installation (Should work on both debian/ubuntu/arch and windows)

 - Install anaconda https://www.anaconda.com/download
 - Install unity-hub https://docs.unity3d.com/hub/manual/InstallHub.html
 - Create a conda environment with the right python version: conda create -n mlagents python=3.10.12 && conda activate mlagents
 - Install pytorch, cuda, and mlagents
    > pip3 install torch~=2.2.1 --index-url https://download.pytorch.org/whl/cu121
    > pip3 install mlagents

## Test run

 - Launch unity hub
 - Go to projects > Add (top right corner grey button) > Add project from disk, then select the directory called "navigation_envs_unity_project" from this directory.
 IMPORTANT: Do not select THIS directory (it will just print an error message), select the SUBFOLDER named "navigation_envs_unity_project"
 - Open the scene of one of the environments (example: ./Assets/PointMaze/Scenes/PointMaze) but selecting it in the file navigator in the bottom-left, and double click on the scene file.

 - Now, open a terminale, and change the directory to this one.
 - Activate the conda environment you created earlier,
 - Launch a training, ex: mlagents-learn config/point_maze_sac.yaml --run-id="my_first_run"
    Note: The first argument is the config file (hyperparameters and other stuff). Choose the right one according to the scene you choose (in the config directory).
    Note 2: if you already launched this command, then a run with this id have been saved. Add --force if you want to erase the old one, --resume if you want to resume it, or change the id in the command.
    
 - If everything works, you should have a unity logo in your terminal, and a line telling that the program is listening at port 5004. Click on the play button at the top of the unity editor to launch the training.
 - Wait for the agent to converge!

## File description
