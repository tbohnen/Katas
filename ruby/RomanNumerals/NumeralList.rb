require_relative 'RomanNumeral'

class NumeralList
  include Enumerable

  def initialize
    initialiseNumeralList()
  end

  def initialiseNumeralList()
    numeralM = RomanNumeral.new("M",1000)
    numeralCM = RomanNumeral.new("CM",900)
    numeralD = RomanNumeral.new("D",500)
    numeralCD = RomanNumeral.new("CD",400)
    numeralC = RomanNumeral.new("C",100)
    numeralXC = RomanNumeral.new("XC",90)
    numeralL = RomanNumeral.new("L",50)
    numeralXL = RomanNumeral.new("XL",40)
    numeralX = RomanNumeral.new("X",10)
    numeralIX = RomanNumeral.new("IX",9)
    numeralV = RomanNumeral.new("V",5)
    numeralIV = RomanNumeral.new("IV",4)
    numeralI = RomanNumeral.new("I",1)
    @roman_numeral_list = [numeralM, numeralCM, numeralD, numeralCD, numeralC, numeralXC,
                           numeralL, numeralXL, numeralX, numeralIX, numeralV, numeralIV, numeralI]
  end

  def each &block
    @roman_numeral_list.each {|member| block.call(member)}
  end

end
