using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Concrete;
using Domain.Entities;

namespace Domain.Data
{
    public static class DbInitializer
    {

        public static List<MetodoPagamentoViewHelp> GetMetodosPagamento()
        {
            var list = new List<MetodoPagamentoViewHelp>
            {
                new MetodoPagamentoViewHelp
                {
                    PagamentoId = MetodoPagamentoViewHelp.MetodoPagamento.Transferencias,
                    Foto = "referencias.png",
                    Descricao = "Pagamentos por referências"
                },
                //new MetodoPagamentoViewHelp
                //{
                //    PagamentoId = MetodoPagamentoViewHelp.MetodoPagamento.Paykwz,
                //    Foto = "paykz.png",
                //    Descricao = "PAYKZ"
                //}
            };
            return list;
        }

        public static List<PartnersViewHelp> GetPartners()
        {
            var list = new List<PartnersViewHelp>
            {
                new PartnersViewHelp
                {
                    Entidade = "NOpcoes",
                    Logo = "logo-nopcoes.png",
                    Link = "https://facebook.com/opcoesn/"
                },
                new PartnersViewHelp
                {
                    Entidade = "NOpcoes",
                    Logo = "logo-nopcoes.png",
                    Link = "https://facebook.com/opcoesn/"
                },
                new PartnersViewHelp
                {
                    Entidade = "NOpcoes",
                    Logo = "logo-nopcoes.png",
                    Link = "https://facebook.com/opcoesn/"
                },
                new PartnersViewHelp
                {
                    Entidade = "NOpcoes",
                    Logo = "logo-nopcoes.png",
                    Link = "https://facebook.com/opcoesn/"
                },
            };
            return list;
        } 
        public static void Initialize(MovimentaContext context)
        {
            context.Database.CreateIfNotExists();

            //if (!context.Carrossels.Any())
            //{
            //    var slides = new[]
            //    {
            //    new Carrossel
            //    {
            //        Content = ""
            //    }
            //};

            //    foreach (var slide in slides)
            //    {
            //        context.Carrossels.Add(slide);
            //    }
            //    context.SaveChanges();
            //}

            if (!context.Startups.Any())
            {
                var startups = new Startup[]
                {
                    new Startup {Nome = "Movimenta",AreaActuacao = "Fintech"},
                    new Startup {Nome = "Independente",AreaActuacao = "Diversos"},
                };

                foreach (var srtup in startups)
                {
                    context.Startups.Add(srtup);
                }
                context.SaveChanges();
            }

            if (!context.Sucessos.Any() && context.Membros.Any())
            {
                var sucessos = new[]
                {
                    new Sucesso {
                        MembroId = 1,

                        Opiniao = "Loren vitae vitae vitae vitae vitae vitae vitae vitae vitae vitae",
                        Data = DateTime.Now
                    },
                    new Sucesso {
                        MembroId = 1,
                        Opiniao = "Loren vitae vitae vitae vitae vitae vitae vitae vitae vitae vitae",
                        Data = DateTime.Now
                    }
                };
                foreach (var sucesso in sucessos)
                {
                    context.Sucessos.Add(sucesso);
                }
                context.SaveChanges();
            }

            if (!context.Campanhas.Any())
            {
                var campanhas = new[]
                {
                    new Campanha {Nome = "Tudo ou Nada"},
                    new Campanha {Nome = "Flexivel"}
                };
                foreach (var elem in campanhas)
                {
                    context.Campanhas.Add(elem);
                }
                context.SaveChanges();
            }

            if (!context.Categorias.Any())
            {
                var categorias = new []
                {
                    new Categoria {nome = "Teatro e Dança"},
                    new Categoria {nome = "Socioambiental"},
                    new Categoria {nome = "Pessoais"},
                    new Categoria {nome = "Música"},
                    new Categoria {nome = "Literatura"},
                    new Categoria {nome = "Jornalismo"},
                    new Categoria {nome = "Jogos"},
                    new Categoria {nome = "Gastronomia"},
                    new Categoria {nome = "Educação"},
                    new Categoria {nome = "Cinema e Vídeo"},
                    new Categoria {nome = "Ciência e Tecnologia"},
                    new Categoria {nome = "Artes"},
                    new Categoria {nome = "Arquitectura e Urbanismo"},
                    new Categoria {nome = "Ajuda Humanitária"}
                   
                };

                foreach (var categoria in categorias)
                {
                    context.Categorias.Add(categoria);
                }
                context.SaveChanges();
            }

            if (!context.Paises.Any())
            {
                var paises = new Pais[]
                {
                    new Pais {Nome = "Angola"},
                    new Pais {Nome = "Cuba"}
                };
                foreach (var element in paises)
                {
                    context.Paises.Add(element);
                }
                context.SaveChanges();
            }
            if (!context.Provincias.Any())
            {
                var provincias = new Provincia[]
                {
                    //provinvincias Angola
                    new Provincia { Nome = "Bengo", IdPais = 1},
                    new Provincia { Nome = "Benguela", IdPais = 1},
                    new Provincia { Nome = "Bié", IdPais = 1},
                    new Provincia { Nome = "Cabinda", IdPais = 1},
                    new Provincia { Nome = "Cuando-Cubango", IdPais = 1},
                    new Provincia { Nome = "Cuanza Norte", IdPais = 1},
                    new Provincia { Nome = "Cuanza Sul", IdPais = 1},
                    new Provincia { Nome = "Cunene", IdPais = 1},
                    new Provincia { Nome = "Huambo", IdPais = 1},
                    new Provincia { Nome = "Huíla", IdPais = 1},
                    new Provincia { Nome = "Luanda", IdPais = 1},
                    new Provincia { Nome = "Lunda Norte", IdPais = 1},
                    new Provincia { Nome = "Lunda Sul", IdPais = 1},
                    new Provincia { Nome = "Malanje", IdPais = 1},
                    new Provincia { Nome = "Moxico", IdPais = 1},
                    new Provincia { Nome = "Namibe", IdPais = 1},
                    new Provincia { Nome = "Uíge", IdPais = 1},
                    new Provincia { Nome = "Zaire", IdPais = 1},
                    //provinvincias Cuba
                    new Provincia { Nome = "Pinar del Río", IdPais = 2},
                    new Provincia { Nome = "Artemisa", IdPais = 2},
                    new Provincia { Nome = "La Habana", IdPais = 2},
                    new Provincia { Nome = "Mayabeque", IdPais = 2},
                    new Provincia { Nome = "Matanzas", IdPais = 2},
                    new Provincia { Nome = "Cienfuego", IdPais = 2},
                    new Provincia { Nome = "Villa Clara", IdPais = 2},
                    new Provincia { Nome = "Sacti Spíritus", IdPais = 2},
                    new Provincia { Nome = "Ciego de Ávila", IdPais = 2},
                    new Provincia { Nome = "Camaguey", IdPais = 2},
                    new Provincia { Nome = "Las Tunas", IdPais = 2},
                    new Provincia { Nome = "Holguín", IdPais = 2},
                    new Provincia { Nome = "Santiago de Cuba", IdPais = 2},
                    new Provincia { Nome = "Guantánamo", IdPais = 2},
                    new Provincia { Nome = "Granma", IdPais = 2},
                    new Provincia { Nome = "Isla de la Juventud", IdPais = 2}
                };

                foreach (var element in provincias)
                {
                    context.Provincias.Add(element);
                }
                context.SaveChanges();
            }

            if (!context.Municipios.Any())
            {
                var municipiosStr = new []
                {
                    "Ambriz,Bula Atumba,Dande,Dembos,Nambuangongo,Pango Aluquém",
                    "Balombo,Baía Farta,Benguela,Bocoio,Caimbambo,Catumbela,Chongoroi,Cubal,Ganda,Lobito",
                    "Andulo,Camacupa,Catabola,Chinguar,Chitembo,Cuemba,Cunhinga,Cuíto,Nharea",
                    "Belize,Buco-Zau,Cabinda,Cacongo",
                    "Calai,Cuangar,Cuchi,Cuito Cuanavale,Dirico,Mavinga,Menongue,Rivungo",
                    "Ambaca,Banga,Bolongongo,Cambambe,Cazengo,Golungo Alto,Gonguembo,Lucala,Quiculungo,Samba Caju",
                    "Amboim,Cassongue,Cela,Conda,Ebo,Libolo,Mussende,Porto Amboim,Quibala,Quilenda,Seles,Sumbe",
                    "Cahama,Cuanhama,Curoca,Cuvelai,Namacunde,Ombandja",
                    "Bailundo,Cachiungo,Caála,Ecunha,Huambo,Londuimbali,Loconjo,Mungi,Chicala-Choloanga,Chinjenje,Ucuma",
                    "Caconda,Cacula,Caluquembe,Chiange,Chibia,Chiconda,Chipinho,Cuvango,Humpata,Jamba,Lubango,Matala,Quilengues,Quipungo",
                    "Belas,Cacuaco,Cazenga,Ícolo e Bengo,Luanda,Quilamba Quiaxi,Quissama,Talatona,Viana",
                    "Cambulo,Capenda-Camulemba,Caungula,Chitato,Cuango,Cuílo,Lóvua,Lubalo,Lucapa,Xá-Muteba",
                    "Cacolo,Dala,Muconda,Saurimo",
                    "Cacuso,Calandula,Cambundi-Catembo,Cangandala,Caombo,Cuaba Nzoji,Cunda-Dia-Baze,Luquembo,Malanje,Marimba,Massango,Mucari,Quela,Quirina",
                    "Alto Zambeze,Bundas,Bundas,Camanongue, Léua,Luau,Luacano,Luchazes,Cameia,Moxico",
                    "Bibala,Camucuio,Moçamedes,Tômbua,Virei",
                    "Alto Cauale,Ambuila,Bembe,Buengas,Bungo,Damba,Milunga,Mucaba,Negaje,Puri,Quimbele,Quitaxe,Sanza Pombo, Songo, Uíge,Zombo",
                    "Cuimba,Mbanza Congo,Nóqui,Nzeto,Soio,Tomboco",
                    "Consolación del Sur,Guane,La Palma,Los Palacios,Mantua,Minas de Matahambre,Pinar del Río,San Juan y Martínez,San Luis,Sandino,Viñales",
                    "Alquizar,Artemisa,Bahía Honda,Bauta,Caimanito,Candelaria,Guanajay,Guira de Melena,Mariel,San Antonio de los Baños,San Cristóbal",
                    "Arroyo Naranjo,Boyeros,CEntro Habana,Cerro,Cotorro,Diez de Octubre,Guanabacoa,La Habana del Este,La Habana Vieja,La Lisa,Marianao,Playa,Plaza de la Revolución,Regla,San Miguel del Padrón",
                    "Batabanó,Bejucal,Guines,Jaruco,Madruga,Melena de Sur,Nueva Paz,Quivicán,San José de las Lajas,San Nicolás,Santa Cruz del Norte",
                    "Calimanete,Cárdenas,Ciénega de Zapata,Colón,Jaguey Grande,Jovellanos,Limonar,Los Arabos,Martí,Matanzas,Pedro Bentacourt,Perico,Unión de Reyes",
                    "Abreus,Aguada de Pasajeros,Cienfuegos,Cruces,Cunamanayagua,Lajas,Palmira,Rodas",
                    "Caibarién,Camajuaní,Cifuentes,Corralillo,Encrucijada,Manicaragua,Placetas,Quemado de Guines,Ranchuelo,San Juan de los Remedios,Sagua la Grande,Santa Clara,Santo Domingo",
                    "Cabaiguán,Fomento,Jatibonico,La Sierpe,Santi Spíritus,Taguasco,Trinidad,Yaguay",
                    "Baraguá,Bolivia,Chambas,Ciego de Ávila,Ciro Redondo,Florencia,Majagua,Morón,Primero de Enero,Venezuela",
                    "Camaguey,Carlos M. de Céspedes,Esmeralda,Florida,Guáimaro,Jimaguayú,Minas,Najasa,Nuevitas,Santa Cruz del Sur,Sibanicú,Sierra de Cubitas,Vertientes",
                    "Amancio,Colombia,Jesús Menéndez,Jobabo,Majibacoa,Manatí,Puerto Padre,Las Tunas",
                    "Antilla,Báguanos,Banes,Cacocum,Calixto García,Cueto,Frank País,Gibara,Holguín,Mayarí,Moa,Rafael Freyre,Sagua de Tánamo,Urbano Noris",
                    "Bartolomé Masó,Bayamo,Buey Arriba,Campechuela,Cauto Cristo,Guisa,Jiguaní,Manzanillo,Media Luna,Niquero,Pilón,Río Cauto,Yara",
                    "Contramaestre,Guamá,Mella,Palma Soriano,San Luis,Santiago de Cuba,Segundo Frente,Songo-La Maya,Tercer Frente",
                    "Baracoa,Caimanera,El Salvador,Guntánamo,Imías,Maisí,Manuel Tames,Niceto Pérez,Santónio del Sur,Yateras",
                    "Isla de la Juventud"
                };

                var municipios = new List<Municipio>();
                var provincias = context.Provincias.ToList();
                for (var i = 0; i < provincias.Count; i++)
                {
                    var municipiosProv = municipiosStr[i].Split(',').ToList();
                    municipios.AddRange(municipiosProv.Select(mun => new Municipio {Nome = mun, IdProvincia = provincias[i].IdProvincia}));
                }
                foreach (var element in municipios)
                {
                    context.Municipios.Add(element);
                }
                context.SaveChanges();
            }
        }
    }
}