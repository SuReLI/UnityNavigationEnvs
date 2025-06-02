import sys
import os
import numpy as np
import pathlib
import yaml

script_dir = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, os.path.abspath(os.path.join(script_dir, "../..")))


from mlagents.plugins.trainer_type import register_trainer_plugins
from mlagents.trainers.ppo.trainer import PPOTrainer
from mlagents.trainers.settings import RunOptions
from mlagents.trainers.learn import run_training
register_trainer_plugins()
configuration_path = os.path.join(script_dir, "point_maze_ppo.yaml")  # update with correct YAML file name
configuration_file = open(configuration_path, 'r')
configuration = yaml.safe_load(configuration_file)
configuration["env_settings"]["env_path"] = os.path.join(script_dir, "../../build/point_maze/point_maze.x86_64")
options = RunOptions.from_dict(configuration)  # Convert to RunOptions
run_seed = options.env_settings.seed
if run_seed == -1:
    run_seed = np.random.randint(0, 10000)
num_areas = options.env_settings.num_areas
run_training(run_seed, options, num_areas)