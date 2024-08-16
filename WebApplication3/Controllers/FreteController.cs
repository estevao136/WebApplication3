using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;


[ApiController]
[Route("api/frete")]
public class FreteController : ControllerBase
{
    private const float TaxaPorCm3 = 0.01f;

    private static readonly Dictionary<string, float> TarifaPorEstado = new Dictionary<string, float>
    {
        { "SP", 50.0f },
        { "RJ", 60.0f },
        { "MG", 55.0f },
        { "OUTROS", 70.0f }
    };

    [HttpPost]
    public IActionResult CalcularFrete([FromBody] classfrete classfrete)
    {
        if (classfrete == null)
        {
            return BadRequest("Dados inválidos.");
        }

        float volume = classfrete.Altura * classfrete.Largura * classfrete.Comprimento;
        float tarifaEstado = TarifaPorEstado.ContainsKey(classfrete.UF.ToUpper()) ?
                              TarifaPorEstado[classfrete.UF.ToUpper()] :
                              TarifaPorEstado["OUTROS"];

        float valorFrete = (volume * TaxaPorCm3) + tarifaEstado;

        return Ok(new
        {
            Classfrete = classfrete.Nome,
            Volume = volume,
            ValorFrete = valorFrete
        });
    }
}