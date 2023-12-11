# Caterpillar Control System

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![C# Version](https://img.shields.io/badge/c%23-8.0-blue)](https://docs.microsoft.com/en-us/dotnet/csharp/)

[![.NET Unit Tests](https://github.com/Njuguna-JohnBrian/CaterpillarControlSystem/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Njuguna-JohnBrian/CaterpillarControlSystem/actions/workflows/dotnet.yml)

## Overview

The Caterpillar Control System is a console-based simulation of a caterpillar's movement. The system allows users to control the caterpillar by providing commands for movement, growth, and shrinking.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Command List](#command-list)
- [Sample Movements](#sample-movements)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Caterpillar Simulation:** Simulate the movement, growth, and shrinking of a caterpillar on a console-based grid.
- **Interactive Console Interface:** Receive user commands through a console interface.
- **Obstacles and Boosters:** Navigate through obstacles and collect boosters for caterpillar manipulation.
- **Logging:** Log movements, errors, and general actions to different log files.

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Njuguna-JohnBrian/CaterpillarControlSystem
   ```

2. Navigate to the project directory:

   ```bash
   cd CaterpillarControlSystem
   ```

3. Compile and run the application:

   ```bash
   dotnet run --project CaterpillarControlSystem.control 
   ```

## Usage

1. Run the application as instructed in the installation section.
2. Follow the on-screen instructions to provide commands for caterpillar movement, growth, and shrinking.
3. View the console output to see the caterpillar's current state and radar image.

## Command List

- `U`: Move Up
- `D`: Move Down
- `L`: Move Left
- `R`: Move Right
- `G`: Grow Caterpillar
- `S`: Shrink Caterpillar
- `Q`: Quit

## Sample Movements

Here is an example series of motions you can try:

```bash
U: Move Up
D: Move Down
L: Move Left
R: Move Right
G: Grow Caterpillar
S: Shrink Caterpillar
Q: Quit
```

This series of motions moves the caterpillar in different directions and includes growth and shrinking actions.

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests to improve the project.

## License

This project is licensed under the MIT License
