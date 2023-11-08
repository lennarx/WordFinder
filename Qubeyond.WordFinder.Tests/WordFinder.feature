Feature: Word Finder tests

As a User, I want to be able to create a word finder and to find words contained on it

#Word Finder creation
Scenario: Matrix successfully created
	Given I have a matrix to create the WordFinder
	When I try to create the matrix
	Then I should the matrix created

Scenario: Creation of Matrix fails due to empty matrix
	Given I have a matrix to create the WordFinder
		But the matrix is empty
	When I try to create the matrix
	Then I should see an exception thrown
		And it should contain the message Provided matrix is empty

Scenario: Creation of Matrix fails due to words of different size
	Given I have a matrix to create the WordFinder
		But the matrix has words with different sizes
	When I try to create the matrix
	Then I should see an exception thrown
		And it should contain the message Rows contained in matrix do not have the same length

Scenario: Creation of matrix fails due to words containing non letter characters
	Given I have a matrix to create the WordFinder
		But the matrix contains non letter characters
	When I try to create the matrix
	Then I should see an exception thrown
		And it should contain the message One of the provided words contains one or more non letter character

Scenario: Creation of Matrix fails due to size limit exceeded
	Given I have a matrix to create the WordFinder
		But the matrix rows and columns exceeds the size limit
	When I try to create the matrix
	Then I should see an exception thrown
		And it should contain the message Provided matrix exceeds the matrix size limits


#Words search
Scenario: I search for words into the matrix
	Given I have a matrix to create the WordFinder
		And the Wordfinder is created
		And I have words included in the matrix
		And I have exluded words in the matrix
	When I try to search the words in the matrix
	Then I should see the results 
		And the lists should match
	
