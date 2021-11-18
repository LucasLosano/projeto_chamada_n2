using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MongoDB.Driver;
using Volare.Models;

namespace Volare.DAO
{
    public static class MongoDAO
    {
        public static List<ChamadaDTO> GetChamadaData()
        {
            List<ChamadaDTO> chamadasDTO = new List<ChamadaDTO>();
            try
            {
                MongoClient dbClient = new MongoClient("mongodb://helix:H3l1xNG@54.232.132.193:27000/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
                var db = dbClient.GetDatabase("sth_helixiot");
                IMongoCollection<MongoViewModel> collection = db.GetCollection<MongoViewModel>("sth_/_4_iot");
                IList<MongoViewModel> documents = collection.Find(c => c.Tipo != "vazio").ToList();
                for(int i=0; i < documents.Count; i+=4)
                {
                    chamadasDTO.Add(new ChamadaDTO(){
                        AlunoId = documents[i].ValorId, 
                        SalaId = documents[i+1].ValorId, 
                        Chegada = documents[i].DataRegistro.AddHours(-3)
                    });
                }

                collection.UpdateMany(c => true, Builders<MongoViewModel>.Update.Set(s => s.Tipo, "vazio"));
                GC.SuppressFinalize(db);
                return chamadasDTO;
            }
            catch
            {
                return chamadasDTO;
            }
        }
    }
}