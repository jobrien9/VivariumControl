using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace FrogApp
{
    public class ParticleCommunication
    {
        public static LightStatus GetFrogletLightStatus()
        {
            using (var httpClient = new HttpClient())
            {
                var requestString = $"{App.BASE_PARTICE_URL}isLightOn?access_token={App.PARTICLE_ACCESS_TOKEN}";
                var result = httpClient.PostAsync(requestString, null).Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultAsJson = JsonConvert.DeserializeObject<ParticleResponse>(result.Content.ReadAsStringAsync().Result);
                    //if return value is 1, the light is on
                    return resultAsJson.ReturnValue == 1 ? LightStatus.On : LightStatus.Off;
                }
                else
                {
                    throw new Exception("Could not connect to Particle for Froglet Light Status!");
                }
            }
        }

        public static LightStatus GetVivariumLightStatus()
        {
            using (var httpClient = new HttpClient())
            {
                var requestString = $"{App.BASE_PARTICE_URL}getVivariumLightStatus?access_token={App.PARTICLE_ACCESS_TOKEN}";
                var result = httpClient.PostAsync(requestString, null).Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultAsJson = JsonConvert.DeserializeObject<ParticleResponse>(result.Content.ReadAsStringAsync().Result);
                    //if return value is 1, the light is on
                    if (resultAsJson.ReturnValue == 2)
                    {
                        return LightStatus.Twilight;
                    }
                    else if (resultAsJson.ReturnValue == 1)
                    {
                        return LightStatus.On;
                    }
                    else
                    {
                        return LightStatus.Off;
                    }
                }
                else
                {
                    throw new Exception("Could not connect to Particle for Froglet Light Status!");
                }
            }
        }
    }
}