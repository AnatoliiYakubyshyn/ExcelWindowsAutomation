Feature: Insert date and time

Scenario: Insert and verify date and time
    Given I am on Document page with clean document
    When I insert the current date and time
    Then the current date and time should be displayed in the document
