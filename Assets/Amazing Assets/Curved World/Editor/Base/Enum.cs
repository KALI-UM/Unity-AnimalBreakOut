// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>

namespace AmazingAssets.CurvedWorld.Editor
{
    static public class Enum
    {
        public enum RenderPipeline { Unknown, Builtin, Universal, HighDefinition }
        public enum Tab { Controllers, ShaderIntegration, RenderersOverview, CurvedWorldKeywords, Activator, About }
        public enum RenderersOverviewMode { Scene, SelectedOjects }
        public enum SearchOption { ShaderName, MaterialName, Keyword }
        public enum Extension { cginc, UnityShaderGraphNormal, UnityShaderGraphVertex, AmplifyShaderEditorNormal, AmplifyShaderEditorVertex }
        public enum KeywordsCompile { Default, ShaderFeature, MultiCompile }
        public enum ActivationState { Done, Skip, Problem }
        public enum NormalTransformation { Default, Enable, Disable }
    }
}
