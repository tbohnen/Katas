require_relative 'RomanNumerals'

describe "this is to test the roman numeral converter" do
	it "converts 1 to I" do

			numberConverter = NumberConverter.new
			expect(numberConverter.convertToRoman(1)).to eq "I"
		end

		it "converts 2 to II" do

			numberConverter = NumberConverter.new

			convertedValue = numberConverter.convertToRoman 2

			expect(convertedValue).to eq "II"
		end


   it "converts 10 to X" do
			numberConverter = NumberConverter.new

			convertedValue = numberConverter.convertToRoman 10

			expect(convertedValue).to eq "X"
   end


   it "converts 6 to VI" do
     numberConverter = NumberConverter.new

     convertedValue = numberConverter.convertToRoman 6

     expect(convertedValue).to eq "VI"
   end

   it "converts 9 to IX" do
     numberConverter = NumberConverter.new

     convertedValue = numberConverter.convertToRoman 9

     expect(convertedValue).to eq "IX"
   end

   it "converts 16 to XVI" do
     numberConverter = NumberConverter.new

     convertedValue = numberConverter.convertToRoman 16

     expect(convertedValue).to eq "XVI"
   end

   it "converts 556 to DLVI" do
     numberConverter = NumberConverter.new

     convertedValue = numberConverter.convertToRoman 556

     expect(convertedValue).to eq "DLVI"
   end

   it "converts 1234 to MCCXXXIV" do
     numberConverter = NumberConverter.new

     convertedValue = numberConverter.convertToRoman 1234

     expect(convertedValue).to eq "MCCXXXIV"
   end

   #MMCMXCIX
   it "converts 2999 to MMCMXCIX" do
     numberConverter = NumberConverter.new

     convertedValue = numberConverter.convertToRoman 2999

     expect(convertedValue).to eq "MMCMXCIX"
   end
end
