using awing_test.Server.Models.DTOs;
using awing_test.Server.Models.Entities;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace awing_test.Server.Mappers
{
    public class CalculateMapper
    {
        public CalculateEntity RequestDTOToEntity(CalculateRequestDTO calculateRequestDTO)
        {
            CalculateEntity calculateEntity = new CalculateEntity()
            {
                M = calculateRequestDTO.M,
                N = calculateRequestDTO.N,
                P = calculateRequestDTO.P,
                Matrix = JsonSerializer.Serialize(calculateRequestDTO.Matrix)
            };

            return calculateEntity;
        }

        public List<CalculateDTO> EntityToListDTO(List<CalculateEntity> calculateEntities)
        {
            List<CalculateDTO> calculates = new List<CalculateDTO>();

            foreach (var entity in calculateEntities)
            {
                CalculateDTO calculateDTO = new CalculateDTO()
                {
                    N = entity.N,
                    M = entity.M,
                    P = entity.P,
                    Matrix = JsonConvert.DeserializeObject<int[][]>(entity.Matrix),
                    CreatedAt = entity.CreatedAt
                };

                calculates.Add(calculateDTO);
            }

            return calculates;
        }
    }
}