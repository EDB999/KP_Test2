using System;
using System.Collections.Generic;

namespace KP_Test2;

public partial class Client
{
    string login, pass, pass_conf, name,surname, contact, email;
    int card;
    public Client();

    public int IdClient { get; set; }

    public string? Login {   
        get { return login; }
        set { login = value; } 
    }

    public string? Password {
        get { return pass; }
        set { pass = value; }
    }

    public string? PassEntry {
        get { return pass_conf; }
        set { pass_conf = value; }
    }

    public string? Name {
        get { return name; }
        set { name = value; }
    }

    public string? Surname {
        get { return surname; }
        set { surname = value; }
    }

    public string? Contact {
        get { return contact; }
        set { contact = value; }
    }

    public string? Email {
        get { return email; }
        set { email= value; }
    }

    public int? Card {
        get { return card; }
        set { card = card; }
    }

    public string? RoleType = null!;
}