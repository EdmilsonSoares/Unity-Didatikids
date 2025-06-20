using System.Collections.Generic;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
public class Crianca : Connection
{
    public int IdCrianca { get; set; }
    public string Nome { get; set; }
    public string DtNascimento { get; set; }
    public int IdResponsavel { get; set; }
    public int IdFaixaEtaria { get; set; }
    public List<string> TopicosInteresse { get; set; }
    public int? TempoTela { get; set; }
    public int? IdDificuldade { get; set; }
    public string Avatar { get; set; }
    public string Access { get; set; }
    public string Refresh { get; set; }

    public Crianca(int id_crianca, string nome, string dt_nascimento, int id_responsavel, int id_faixa_etaria, List<string> topicos_interesse,
    string access, string refresh) : base()
    {
        this.IdCrianca = id_crianca;
        this.Nome = nome;
        this.DtNascimento = dt_nascimento;
        this.IdResponsavel = id_responsavel;
        this.IdFaixaEtaria = id_faixa_etaria;
        this.TopicosInteresse = topicos_interesse;
        this.Access = access;
        this.Refresh = refresh;
    }

    internal class AtividadeResponse
    {
        [JsonProperty("id_atividade")]
        public int IdAtividade { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("faixa_etaria")]
        public string FaixaEtaria { get; set; }

        [JsonProperty("topico")]
        public string Topico { get; set; }

        [JsonProperty("concluida")]
        public bool Concluida { get; set; }
    }

    private class CompletarAtividadeRequest
    {
        [JsonProperty("concluida")]
        public bool Concluida { get; set; }
    }

    internal async Task<List<AtividadeResponse>> GetAtividades()
    {
        var route = $"/criancas/{this.IdCrianca}/atividades/";
        var response = await Get(route, Access, Refresh);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Erro da API: " + responseBody);
            throw new HttpRequestException($"Erro {response.StatusCode}: {response.ReasonPhrase}");
        }

        var atividades = JsonConvert.DeserializeObject<List<AtividadeResponse>>(responseBody);

        return atividades!;
    }

    internal async Task<string> CompletarAtividade(int id_atividade)
    {
        var route = $"/criancas/{IdCrianca}/atividades/{id_atividade}/completar/";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Access);
        var request = new CompletarAtividadeRequest
        {
            Concluida = true
        };
        var json = JsonConvert.SerializeObject(request);

        try
        {
            var response = await Post(route, json);
        }
        catch (HttpRequestException ex)
        {
            if (ex.Message.Contains("401") || ex.Message.Contains("Unauthorized"))
            {
                var newAccessToken = await RefreshAccessToken(Refresh);
                if (newAccessToken != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                    var response = await Post(route, json);
                }
                else
                {
                    return "Erro: não foi possível renovar o token.";
                }
            }
            else
            {
                return $"Erro ao enviar requisição: {ex.Message}";
            }
        }

        return "Atividade concluída com sucesso.";
    }

    //internal async Task<string> AtualizarCrianca(string access) 
}

