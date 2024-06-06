Feature: Type text
    Scenario: Check text content
        Given I am on document page with clean document
        When I type <text>
        Then <text> is displayed
    Examples:
        | text |
        | hello world! |
        | good bye |
        | Hello |
