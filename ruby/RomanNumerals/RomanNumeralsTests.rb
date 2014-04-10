require_relative 'RomanNumerals'

describe "this is to test the roman numeral converter" do
		it "converts 1 to I" do

			numberConverter = NumberConverter.new
			expect(numberConverter.convertToRoman 1).to eq "I"	
		end

		it "converts 2 to II" do

			numberConverter = NumberConverter.new

			convertedValue = numberConverter.convertToRoman 2

			expect(convertedValue).to eq "II"
		end

end
