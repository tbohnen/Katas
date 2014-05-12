require_relative 'NumeralList'

class NumberConverter

  def initialize
    @roman_numeral_list = NumeralList.new
  end

	def convertToRoman value_to_convert
    return_result = ""

    @roman_numeral_list.each do |numeral|

      numeral_repeat_times = value_to_convert / numeral.decimal

      if numeral_repeat_times > 0
        return_result = AddNumeralsToReturnResult(numeral, numeral_repeat_times, return_result)
        value_to_convert = value_to_convert % numeral.decimal
      end

    end

    return return_result
  end

  def AddNumeralsToReturnResult(numeral, numeral_repeat_times, return_result)
    numeral_repeat_times.times do
      return_result = return_result + numeral.numeral
    end

    return return_result
  end
end

