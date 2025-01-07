// 仇恨棍
// HatredStick.HatredStickBlock
using System.Collections.Generic;
using Engine;
using Engine.Graphics;
using Game;

public class HatredStickBlock : Block
{
    public const int Index = 942;

    public BlockMesh m_standaloneBlockMesh = new BlockMesh();

    public override void Initialize()
    {
        Model model = ContentManager.Get<Model>("Models/Stick");
        Matrix boneAbsoluteTransform = BlockMesh.GetBoneAbsoluteTransform(model.FindMesh("Stick").ParentBone);
        m_standaloneBlockMesh.AppendModelMeshPart(model.FindMesh("Stick").MeshParts[0], boneAbsoluteTransform * Matrix.CreateTranslation(0f, -0.5f, 0f), false, false, false, false, Color.White);
        base.Initialize();
        DefaultDescription = "使用棕色/蓝色棍点击生物来获取目标，当获得俩个目标后点击[编辑]按键即可生成仇恨，让这俩个生物进行扭打\n\n模组名: 仇恨之棍\n版本:1.1\n多人君(Multi-Players)\n\n欢迎关注作者的主页(https://b23.tv/ylNCXdp或https://b23.tv/y9aJ1v)";
    }

    public override IEnumerable<int> GetCreativeValues()
    {
        yield return Terrain.MakeBlockValue(942, 0, 0);
        yield return Terrain.MakeBlockValue(942, 0, 1);
    }

    public override void GenerateTerrainVertices(BlockGeometryGenerator generator, TerrainGeometry geometry, int value, int x, int y, int z)
    {
    }

    public override void DrawBlock(PrimitivesRenderer3D primitivesRenderer, int value, Color color, float size, ref Matrix matrix, DrawBlockEnvironmentData environmentData)
    {
        if (Terrain.ExtractData(value) == 1)
        {
            BlocksManager.DrawMeshBlock(primitivesRenderer, m_standaloneBlockMesh, Color.SkyBlue, 2f * size, ref matrix, environmentData);
        }
        else
        {
            BlocksManager.DrawMeshBlock(primitivesRenderer, m_standaloneBlockMesh, color, 2f * size, ref matrix, environmentData);
        }
    }
}
