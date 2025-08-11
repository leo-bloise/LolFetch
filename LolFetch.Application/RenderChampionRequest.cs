using LolFetch.Core;

namespace LolFetch.Application;

public record RenderChampionRequest(Core.Champion Champion, Canvas Canvas, bool Color, bool Square);