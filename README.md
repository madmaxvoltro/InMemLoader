# InMemLoader

InMemLoader is a tool designed to pull code from a specified website (in this case, localhost), download it into RAM, compile it directly in memory, and execute it—all without writing any files to disk. This project aims to simplify and speed up the process of executing code directly from a remote server or local resources while keeping the operations in memory.

## Features

- Pull code dynamically from a given website (e.g., localhost).
- Compile and execute the code directly in RAM without writing to disk.
- Flexible and lightweight design that works entirely within memory.

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/InMemLoader.git
    ```

2. Open the project in Visual Studio.
3. Build the project in Visual Studio using the default configuration (or any custom configuration you prefer).

## Usage

InMemLoader operates entirely within the code. It doesn't require any command-line input once the application is built. The following operations are handled directly by the code:

- The program pulls code from the specified URL (e.g., localhost).
- The code is downloaded into RAM, compiled, and executed directly in memory.

### Visual Studio Setup

1. Open the project in Visual Studio.
2. Set the necessary URL and port (default is `http://localhost`) within the code.
3. Compile and run the project from Visual Studio.

There is no external output generated, as the code is executed entirely in memory.

## Future Updates

- **AES Encryption Support**: Future versions will include AES encryption, enabling secure transmission and storage of code in memory. This will add an additional layer of security to the code execution process.
  
- **Custom URL Handling**: Support for custom URL formats and additional security features such as SSL/TLS support for fetching code from remote servers.

- **Improved Memory Management**: Further optimization of memory usage to handle larger code payloads efficiently.

## Contribution

We welcome contributions to InMemLoader! If you have any ideas for improvements or features, feel free to fork the repo and create a pull request.

1. Fork the repo
2. Create a new branch (`git checkout -b feature-branch`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature-branch`)
5. Create a new pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- [Your Acknowledgements Here] (e.g., any libraries, inspiration, etc.)
