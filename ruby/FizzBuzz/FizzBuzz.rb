class FizzBuzzPrinter 

  def print_value number

    if number % 3 == 0 and number % 5 == 0
      "FizzBuzz"
	    
    elsif number % 3 == 0
      "Fizz"
	    
    elsif  number % 5 == 0
      "Buzz"
    
    else
      number
    end

  end
end

describe("Fizz buzz output") do

	it "1 Prints 1" do
		printer = FizzBuzzPrinter.new

		converted_Value = printer.print_value 1

		expect(converted_Value).to eq 1
	end
	
	it "3 Prints Fizz" do
          printer = FizzBuzzPrinter.new

	  converted_value = printer.print_value 3

	  expect(converted_value).to eq "Fizz"
	end

	it "5 Prints Buzz" do

          printer = FizzBuzzPrinter.new

	  converted_value = printer.print_value 5 

	  expect(converted_value).to eq "Buzz"
	end

	it "15 Prints FizzBuzz" do

          printer = FizzBuzzPrinter.new

	  converted_value = printer.print_value 15 

	  expect(converted_value).to eq "FizzBuzz"
	end
end

