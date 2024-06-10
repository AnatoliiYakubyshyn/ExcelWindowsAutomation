Feature: View Button Functionality

  Scenario: Check and Uncheck Status Bar and Ruler
    Given I have opened WordPad
    When I click on the View button
    And I check if "Status Bar" and "Ruler" are present
    Then the "Status Bar" and "Ruler" should be present
    When I click on the "Status Bar" and "Ruler" checkboxes
    Then the "Status Bar" and "Ruler" should not be present
