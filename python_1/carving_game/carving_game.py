def main():
    # Step 1: Start the Program
    print("Welcome to the Carving Game!")

    # Step 2: Ask the user if they are okay with using sharp tools
    sharp_tools = input("Are you okay with using sharp tools? (yes/no): ").strip().lower()

    # Step 3: Handle user response
    if sharp_tools == 'no':
        print("Please log out of the game. Thank you for your time!")
        return  # End the program

    elif sharp_tools == 'yes':
        # Step 5: Ask for a username
        username = input("Please choose a username: ").strip()

        # Step 6: Ask the user to choose an object to carve
        objects = ["Wooden Spoon", "Decorative Bowl", "Figurine"]
        print("Choose one object to carve out:")
        for idx, obj in enumerate(objects, start=1):
            print(f"{idx}. {obj}")

        choice = input("Enter the number of your choice: ")

        # Check if the choice is valid
        if choice.isdigit() and 1 <= int(choice) <= len(objects):
            selected_object = objects[int(choice) - 1]
            print(f"{username}, you have chosen to carve a {selected_object}.")
        else:
            print("Invalid choice. Please restart the program.")

    else:
        print("Invalid response. Please answer with 'yes' or 'no'.")

# Run the program
if __name__ == "__main__":
    main()
