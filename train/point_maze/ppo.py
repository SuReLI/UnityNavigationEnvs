import sys

# Add your local project directory to import your local trainers
sys.path.insert(0, "./")  # or the absolute path to your project root

# === Custom algorithm registration ===
from trainers.settings import ALGORITHM_REGISTRY

# === ML-Agents Training CLI ===
from trainers.learn import run_cli

# === Config ===
env_path = "./YourEnv.x86_64"
config_path = "config/sac_her_config.yaml"
run_id = "her-parallel-run"
num_envs = 8
base_port = 5005
no_graphics = True

# === Construct CLI Arguments ===
cli_args = [
    config_path,
    "--run-id", run_id,
    "--env", env_path,
    "--num-envs", str(num_envs),
    "--base-port", str(base_port),
]

if no_graphics:
    cli_args.append("--no-graphics")

# === Run training ===
run_cli(cli_args)
