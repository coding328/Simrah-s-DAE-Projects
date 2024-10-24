import random

def get_valid_choice(prompt, options):
    """
    Function to display options and get a valid choice from the user.

    Parameters:
    - prompt (str): The message to display to the user.
    - options (list): A list of choices for the user to select from.

    Returns:
    - str: The user's selected option.
    """
    while True:
        print(prompt)  # Display the prompt message
        # Display each option with its corresponding number
        for idx, option in enumerate(options, start=1):
            print(f"{idx}. {option}")

        choice = input("Enter the number of your choice: ")

        # Check if the input is a valid choice
        if choice.isdigit() and 1 <= int(choice) <= len(options):
            return options[int(choice) - 1]  # Return the selected option
        else:
            print("Invalid choice. Please select a valid number.")  # Error message for invalid input

def confirm_play_again():
    """
    Function to ask the user if they want to play again and return their response.

    Returns:
    - bool: True if the user wants to play again, False otherwise.
    """
    while True:
        play_again = input("Would you like to play again? (yes/no): ").strip().lower()
        if play_again in ['yes', 'no']:
            return play_again == 'yes'  # Return True if 'yes', False if 'no'
        print("Invalid response. Please answer with 'yes' or 'no'.")  # Error message for invalid input

def main():
    """
    Main function to run the Carving Game.

    This function handles user interactions, including checking if the user is okay with sharp tools,
    prompting for a username, and allowing the user to select an object to carve.
    """
    base_objects = [
        "Wooden Spoon", "Decorative Bowl", "Figurine",
        "Animal Shape", "Flower Pot", "Candle Holder",
        "Keychain", "Coaster", "Jewelry Box",
        "Picture Frame", "Wooden Toy", "Garden Sign",
        "Chess Piece", "Mini Desk Organizer", "Wooden Ring",
        "Guitar Pick", "Plant Stand", "Puzzle Piece",
        "Wine Rack", "Serving Tray", "Birdhouse",
        "Wall Clock", "Treasure Chest", "Sign Post",
        "Napkin Holder", "Cutting Board", "Storage Box",
        "Planter Box", "Magnifying Glass", "Desk Lamp"
    ]
    previous_choices = []  # List to track previous selections
    
    while True:
        # Step 1: Start the Program
        print("Welcome to the Carving Game!")

        # Step 2: Ask the user if they are okay with using sharp tools
        while True:
            sharp_tools: bool = input("Are you okay with using sharp tools? (yes/no): ").strip().lower()  # Boolean variable for response
            if sharp_tools in ['yes', 'no']:
                sharp_tools = sharp_tools == 'yes'  # Convert string to boolean
                break  # Valid response, exit the loop
            print("Invalid response. Please answer with 'yes' or 'no'.")  # Error message for invalid input

        # Step 3: Handle user response
        if not sharp_tools:
            print("Please log out of the game. Thank you for your time!")
            break  # End the program

        # Step 4: If user is okay, ask for a username
        while True:
            username: str = input("Please choose a username: ").strip()  # String variable for username
            if username:  # Ensure username is not empty
                break  # Valid username, exit the loop
            print("Username cannot be empty. Please choose a valid username.")  # Error message for empty input

        # Step 5: Randomize the objects each time the user plays
        objects: list = random.sample(base_objects, min(5, len(base_objects)))  # Show up to 5 options
        selected_object = get_valid_choice("Choose one object to carve out:", objects)

        print(f"{username}, you have chosen to carve a {selected_object}.")
        previous_choices.append(selected_object)  # Track the user's choice

        # Display previous choices
        print(f"Your previous choices: {', '.join(previous_choices)}")

        # Ask the user if they want to play again using the custom function
        if not confirm_play_again():
            print("Thank you for playing! Goodbye!")
            break  # Exit the main loop

# Run the program
if __name__ == "__main__":
    main()







