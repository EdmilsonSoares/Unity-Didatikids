using System.Collections.Generic;
using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;

internal class Responsavel : Connection
{
    internal int? IdResponsavel { get; set; }
    internal string Nome { get; set; }
    internal string Email { get; set; }
    internal string Senha { get; set; }
    internal string DtNascimento { get; set; }
    internal int Codigo { get; set; }
    public string Access { get; set; }
    public string Refresh { get; set; }

    private readonly string RouteLogin = "/login/";
    private readonly string RouteCodigo = "/cadastro/enviar_codigo/";
    private readonly string RouteCadastro = "/cadastro/verificar_codigo/";
    private readonly string RouteCriancas = "/criancas/";
    private readonly string RouteRecuperarSenha = "/recuperacao_senha/enviar_codigo/";
    private readonly string RouteRedefinirSenha = "/recuperacao_senha/verificar_codigo/";

    public class ResponsavelLocal
    {
        public string nome { get; set; }
        public string email { get; set; }
        public string dt_nascimento { get; set; }
        public string senha { get; set; }

    }

    private class LoginRequest
    {
        [JsonProperty("username")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Senha { get; set; }
    }
    internal class LoginResponse
    {
        [JsonProperty("access")]
        public string Access { get; set; }
        [JsonProperty("refresh")]
        public string Refresh { get; set; }
        [JsonProperty("id_responsavel")]
        public int IdResponsavel { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    internal async Task<LoginResponse> LoginAsync()
    {
        var login = new LoginRequest
        {
            Email = Email,
            Senha = Senha
        };

        var json = JsonConvert.SerializeObject(login);

        var response = await Post(RouteLogin, json);

        var responseString = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        var responseBody = JsonConvert.DeserializeObject<LoginResponse>(responseString);

        return responseBody!;
    }

    private class CadastroRequest
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("dt_nascimento")]
        public string DtNascimento { get; set; }
        [JsonProperty("senha")]
        public string Senha { get; set; }
        [JsonProperty("codigo")]
        public int Codigo { get; set; }
    }

    private class EnviarCodigoRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    internal async Task<HttpStatusCode> EnviarCodigoAsync()
    {
        var cadastro = new EnviarCodigoRequest
        {
            Email = Email
        };

        var json = JsonConvert.SerializeObject(cadastro);

        var response = await Post(RouteCodigo, json);

        response.EnsureSuccessStatusCode();

        return response.StatusCode!;
    }

    internal async Task<HttpStatusCode> CadastroAsync()
    {
        var cadastro = new CadastroRequest
        {
            Nome = Nome,
            Email = Email,
            Senha = Senha,
            DtNascimento = DtNascimento,
            Codigo = Codigo
        };

        var json = JsonConvert.SerializeObject(cadastro);

        var response = await Post(RouteCadastro, json);

        response.EnsureSuccessStatusCode();

        return response.StatusCode!;
    }


    internal class CriancaResponse
    {
        [JsonProperty("id_crianca")]
        public int IdCrianca { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("dt_nascimento")]
        public string DtNascimento { get; set; }
        [JsonProperty("id_responsavel")]
        public int IdResponsavel { get; set; }
        [JsonProperty("id_faixa_etaria")]
        public int IdFaixaEtaria { get; set; }
        [JsonProperty("tempo_tela")]
        public int? TempoTela { get; set; }
        [JsonProperty("id_dificuldade")]
        public int? IdDificuldade { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("topicos_interesse")]
        public List<string> TopicosInteresse { get; set; }
    }
    internal async Task<List<CriancaResponse>> GetCriancas()
    {
        var response = await Get(RouteCriancas, Access, Refresh);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Erro da API: " + responseBody);
            throw new HttpRequestException($"Erro {response.StatusCode}: {response.ReasonPhrase}");
        }

        var perfis = JsonConvert.DeserializeObject<List<CriancaResponse>>(responseBody);

        return perfis!;
    }

    public class EnviarCodigoSenhaRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
    public async Task<HttpStatusCode> EnviarCodigoSenhaAsync()
    {
        var request = new EnviarCodigoSenhaRequest { Email = Email };

        var json = JsonConvert.SerializeObject(request);

        var response = await Post(RouteRecuperarSenha, json);

        response.EnsureSuccessStatusCode();

        return response.StatusCode!;
    }

    public class RecuperarSenhaRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("senha")]
        public string Senha { get; set; }
        [JsonProperty("codigo")]
        public int Codigo { get; set; }
    }
    public async Task<HttpStatusCode> RecuperarSenhaAsync()
    {
        var request = new RecuperarSenhaRequest { 
            Email = Email,
            Codigo = Codigo,
            Senha = Senha
        };

        var json = JsonConvert.SerializeObject(request);

        var response = await Post(RouteRedefinirSenha, json);

        response.EnsureSuccessStatusCode();

        return response.StatusCode!;
    }
}

