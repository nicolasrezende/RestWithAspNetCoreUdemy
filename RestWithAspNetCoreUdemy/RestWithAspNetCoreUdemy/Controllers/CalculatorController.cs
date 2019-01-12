using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var subtraction = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(subtraction.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var multiplication = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(multiplication.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var division = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(division.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("square-root/{number}")]
        public IActionResult SquareRoot(string number)
        {
            if (IsNumeric(number))
            {
                var squareRoot = Math.Sqrt((double)ConvertToDecimal(number));
                return Ok(squareRoot.ToString());
            }
            return BadRequest("Invalid Request");
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            if (decimal.TryParse(strNumber, out decimal number))
            {
                return number;
            }
            return 0;
        }

        private bool IsNumeric(string strNumber)
        {
            bool isNumeric = decimal.TryParse(strNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out decimal number);
            return isNumeric;
        }
    }
}