// ChildProfileModel.cs
using System;
using System.Collections.Generic;
using System.Net.WebSockets;

[System.Serializable]
public class ChildModel
{
    public string childNome { get; set; }
    public string childData { get; set; } // Data de nascimento da criança
    public string childTopico { get; set; }
    public string avatarIconPath { get; set; }   // Caminho para o arquivo PNG do avatar

    public ChildModel(string nome, string data, string topico, string iconPath)
    {
        this.childNome = nome;
        this.childData = data;
        this.avatarIconPath = iconPath;
        this.childTopico = topico;
    }
}